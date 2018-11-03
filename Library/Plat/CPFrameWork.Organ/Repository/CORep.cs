﻿using CPFrameWork.Global;
using CPFrameWork.Organ.Domain;
using CPFrameWork.Organ.Infrastructure;
using CPFrameWork.Utility.DbOper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CPFrameWork.Organ.Repository
{
    public abstract class BaseCODepRep : BaseRepository<CODep>
    {
        public BaseCODepRep(ICODbContext dbContext) : base(dbContext)
        {

        }
        public abstract List<COUser> GetUserInDep(int depId);
        public abstract List<CODep> GetDepByUser(int userId);
        public abstract List<CODep> GetAlLDepByDepId(int depId);
    }
    public class CODepRep : BaseCODepRep
    {
        public CODepRep(ICODbContext dbContext) : base(dbContext)
        {

        }

        public override List<COUser> GetUserInDep(int depId)
        {
            CODbContext _db = this._dbContext as CODbContext;
            var q = from dep in _db.CODepCol
                    join relate in _db.CODepUserRelateCol
                    on dep.Id equals relate.DepId
                    join user in _db.COUserCol
                    on relate.UserId equals user.Id
                    orderby relate.ShowOrder ascending
                    where dep.Id.Equals(depId)
                    select user;
            return q.ToList();
        }
        public override List<CODep> GetDepByUser(int userId)
        {
            CODbContext _db = this._dbContext as CODbContext;
            var q = from dep in _db.CODepCol
                    join relate in _db.CODepUserRelateCol
                    on dep.Id equals relate.DepId
                    join user in _db.COUserCol
                    on relate.UserId equals user.Id
                    where user.Id.Equals(userId)
                    select dep;
            return q.ToList();
        }
        public override List<CODep> GetAlLDepByDepId(int depId)
        {
            CODbContext _db = this._dbContext as CODbContext;

            var query = from c in _db.CODepCol
                        where c.Id == depId
                        select c;

            return query.ToList().Concat(query.ToList().SelectMany(t => GetAlLDepByDepId(Convert.ToInt32(t.ParentId)))).ToList();
        }

    }
    public class CODepUserRelateRep : BaseRepository<CODepUserRelate>
    {
        public CODepUserRelateRep(ICODbContext dbContext) : base(dbContext)
        {

        }
    }
    public class COUserRep : BaseRepository<COUser>
    {
        public COUserRep(ICODbContext dbContext) : base(dbContext)
        {

        }
    }
    public class CORoleUserRelateRep : BaseRepository<CORoleUserRelate>
    {
        public CORoleUserRelateRep(ICODbContext dbContext) : base(dbContext)
        {

        }
    }
    public abstract class BaseCORoleRep : BaseRepository<CORole>
    {
        public BaseCORoleRep(ICODbContext dbContext) : base(dbContext)
        {

        }
        public abstract List<CORole> GetUserStaticRoles(int userId);
    }
    public class CORoleRep : BaseCORoleRep
    {
        public CORoleRep(ICODbContext dbContext) : base(dbContext)
        {

        }
        public override List<CORole> GetUserStaticRoles(int userId)
        {
            CODbContext _db = this._dbContext as CODbContext;
            var q = from role in _db.CORoleCol
                    join relate in _db.CORoleUserRelateCol
                    on role.Id equals relate.RoleId
                    join user in _db.COUserCol
                    on relate.UserId equals user.Id
                    where user.Id.Equals(userId)
                    select role;
            return q.ToList();
        }
    }
    public abstract class BaseCOUserIdentityRep : BaseRepository<COUserIdentity>
    {
        public BaseCOUserIdentityRep(ICODbContext dbContext) : base(dbContext)
        {

        }
        public abstract bool DeleteOverdueKey();
    }
    public class COUserIdentityRep : BaseCOUserIdentityRep
    {
        public COUserIdentityRep(ICODbContext dbContext) : base(dbContext)
        {

        }
        public override bool DeleteOverdueKey()
        {

            //string strSql = "delete from CP_UserIdentity where (DATEDIFF(hour, LoginTime, GETDATE()) > 12)";//该语句为删除12小时前的无效数据

            //DbHelper _helper = new DbHelper("CPOrganIns", CPAppContext.CurDbType());
            //_helper.ExecuteNonQuery(strSql);

            //edit by zzh DateDIFF只支持SQLserver,调整为通过EF执行删除操作。

            base.DeleteByCondition(t => t.LoginTime < (DateTime.Now.AddHours(-12))); ;
            return true;
        }
    }
}
