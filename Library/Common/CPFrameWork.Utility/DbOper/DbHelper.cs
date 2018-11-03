using System;
using System.Data;
using System.Diagnostics;
using System.Data.Common;
using System.Data.SqlClient;
using System.Collections;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Configuration;

namespace CPFrameWork.Utility.DbOper
{
    public class DbHelper
    {
        public enum DbTypeEnum
        {
            SqlServer = 1,
            Oracle = 2,
            MySql = 3
        }

        #region  ���Ա���      
        public string ConntionString
        {
            get; set;
        }
        //���ݷ��ʻ�����--���캯��  
        public DbHelper(string dbIns, DbTypeEnum dbType)
        {
            //var builder = new ConfigurationBuilder().AddJsonFile("appsettings.json");
            //var Configuration = builder.Build();          
            //this.ConntionString = Configuration.GetConnectionString(dbIns);
            this.ConntionString = CPUtils.Configuration.GetSection("ConnectionStrings")[dbIns];
            this.DbType = dbType;


        }
        /// <summary>  
        /// ���ݿ�����   
        /// </summary>   
        public DbTypeEnum DbType
        {
            get; set;
        }
        #endregion


        #region ת������  
        private System.Data.IDbDataParameter iDbPara(string ParaName, string DataType)
        {
            switch (this.DbType)
            {
                case DbTypeEnum.SqlServer:
                    return GetSqlPara(ParaName, DataType);
                //case  DbTypeEnum.Oracle:
                //    return GetOleDbPara(ParaName, DataType); 
                case DbTypeEnum.MySql:
                    return GetMySqlPara(ParaName, DataType);
                default:
                    return GetSqlPara(ParaName, DataType);
            }
        }

        private SqlParameter GetSqlPara(string ParaName, string DataType)
        {
            switch (DataType)
            {
                case "Decimal":
                    return new SqlParameter(ParaName, SqlDbType.Decimal);
                case "Varchar":
                    return new SqlParameter(ParaName, SqlDbType.VarChar);
                case "DateTime":
                    return new SqlParameter(ParaName, SqlDbType.DateTime);
                case "Iamge":
                    return new SqlParameter(ParaName, SqlDbType.Image);
                case "Int":
                    return new SqlParameter(ParaName, SqlDbType.Int);
                case "Text":
                    return new SqlParameter(ParaName, SqlDbType.NText);
                default:
                    return new SqlParameter(ParaName, SqlDbType.VarChar);
            }
        }
        private MySqlParameter GetMySqlPara(string ParaName, string DataType)
        {
            switch (DataType)
            {
                case "Decimal":
                    return new MySqlParameter(ParaName, SqlDbType.Decimal);
                case "Varchar":
                    return new MySqlParameter(ParaName, SqlDbType.VarChar);
                case "DateTime":
                    return new MySqlParameter(ParaName, SqlDbType.DateTime);
                case "Iamge":
                    return new MySqlParameter(ParaName, SqlDbType.Image);
                case "Int":
                    return new MySqlParameter(ParaName, SqlDbType.Int);
                case "Text":
                    return new MySqlParameter(ParaName, SqlDbType.NText);
                default:
                    return new MySqlParameter(ParaName, SqlDbType.VarChar);
            }
        }
        //private OracleParameter GetOraclePara(string ParaName, string DataType)
        //{
        //    switch (DataType)
        //    {
        //        case "Decimal":
        //            return new OracleParameter(ParaName, OracleType.Double);
        //        case "Varchar":
        //            return new OracleParameter(ParaName, OracleType.VarChar);
        //        case "DateTime":
        //            return new OracleParameter(ParaName, OracleType.DateTime);
        //        case "Iamge":
        //            return new OracleParameter(ParaName, OracleType.BFile);
        //        case "Int":
        //            return new OracleParameter(ParaName, OracleType.Int32);
        //        case "Text":
        //            return new OracleParameter(ParaName, OracleType.LongVarChar);
        //        default:
        //            return new OracleParameter(ParaName, OracleType.VarChar);

        //    }
        //}
        //private OleDbParameter GetOleDbPara(string ParaName, string DataType)
        //{
        //    switch (DataType)
        //    {
        //        case "Decimal":
        //            return new OleDbParameter(ParaName, System.Data.DbType.Decimal);
        //        case "Varchar":
        //            return new OleDbParameter(ParaName, System.Data.DbType.String);
        //        case "DateTime":
        //            return new OleDbParameter(ParaName, System.Data.DbType.DateTime);
        //        case "Iamge":
        //            return new OleDbParameter(ParaName, System.Data.DbType.Binary);
        //        case "Int":
        //            return new OleDbParameter(ParaName, System.Data.DbType.Int32);
        //        case "Text":
        //            return new OleDbParameter(ParaName, System.Data.DbType.String);
        //        default:
        //            return new OleDbParameter(ParaName, System.Data.DbType.String);
        //    }
        //}
        #endregion
        #region ���� Connection �� Command  
        public IDbConnection GetConnection()
        {
            switch (this.DbType)
            {
                case DbTypeEnum.SqlServer:
                    return new SqlConnection(this.ConntionString);
                //case  DbTypeEnum.Oracle:
                //    return new OracleConnection(this.ConntionString); 
                case DbTypeEnum.MySql:
                    return new MySqlConnection(this.ConntionString);
                default:
                    return new SqlConnection(this.ConntionString);
            }
        }
        private IDbCommand GetCommand(string Sql, IDbConnection iConn)
        {
            switch (this.DbType)
            {
                case DbTypeEnum.SqlServer:
                    return new SqlCommand(Sql, (SqlConnection)iConn);
                //case "Oracle":
                //    return new OracleCommand(Sql, (OracleConnection)iConn); 
                case DbTypeEnum.MySql:
                    return new MySqlCommand(Sql, (MySqlConnection)iConn);
                default:
                    return new SqlCommand(Sql, (SqlConnection)iConn);
            }
        }
        private IDbCommand GetCommand()
        {
            switch (this.DbType)
            {
                case DbTypeEnum.SqlServer:
                    return new SqlCommand();
                //case "Oracle":
                //    return new OracleCommand(); 
                case DbTypeEnum.MySql:
                    return new MySqlCommand();
                default:
                    return new SqlCommand();
            }
        }
        private IDataAdapter GetAdapater(string Sql, IDbConnection iConn)
        {
            switch (this.DbType)
            {
                case DbTypeEnum.SqlServer:
                    return new SqlDataAdapter(Sql, (SqlConnection)iConn);
                //case "Oracle":
                //    return new OracleDataAdapter(Sql, (OracleConnection)iConn); 
                case DbTypeEnum.MySql:
                    return new MySqlDataAdapter(Sql, (MySqlConnection)iConn);
                default:
                    return new SqlDataAdapter(Sql, (SqlConnection)iConn); ;
            }
        }
        private IDataAdapter GetAdapater(IDbCommand cmd, IDbConnection iConn)
        {
            switch (this.DbType)
            {
                case DbTypeEnum.SqlServer:
                    return new SqlDataAdapter(cmd as SqlCommand);
                //case "Oracle":
                //    return new OracleDataAdapter(Sql, (OracleConnection)iConn); 
                case DbTypeEnum.MySql:
                    return new MySqlDataAdapter(cmd as MySqlCommand);
                default:
                    return new SqlDataAdapter(cmd as SqlCommand);
            }
        }
        private IDataAdapter GetAdapater()
        {
            switch (this.DbType)
            {
                case DbTypeEnum.SqlServer:
                    return new SqlDataAdapter();
                //case "Oracle":
                //    return new OracleDataAdapter(); 
                case DbTypeEnum.MySql:
                    return new MySqlDataAdapter();
                default:
                    return new SqlDataAdapter();
            }
        }
        private IDataAdapter GetAdapater(IDbCommand iCmd)
        {
            switch (this.DbType)
            {
                case DbTypeEnum.SqlServer:
                    return new SqlDataAdapter((SqlCommand)iCmd);
                //case "Oracle":
                //    return new OracleDataAdapter((OracleCommand)iCmd);
                case DbTypeEnum.MySql:
                    return new MySqlDataAdapter((MySqlCommand)iCmd);
                default:
                    return new SqlDataAdapter((SqlCommand)iCmd);
            }
        }
        #endregion
        #region  ִ�м�SQL���  
        public int ExecuteNonQuery(IDbCommand cmd)
        {
            using (IDbConnection iConn = this.GetConnection())
            {
                iConn.Open();
                try
                {
                    cmd.Connection = iConn;
                    int rows = cmd.ExecuteNonQuery();
                    return rows;
                }
                catch (System.Exception E)
                {
                    throw new Exception(E.Message);
                }
                finally
                {
                    if (iConn.State != ConnectionState.Closed)
                    {
                        iConn.Close();
                    }
                }
            }
        }
        /// <summary>  
        /// ִ��SQL��䣬����Ӱ��ļ�¼��  
        /// </summary>  
        /// <param name="SQLString">SQL���</param>  
        /// <returns>Ӱ��ļ�¼��</returns>  
        public int ExecuteNonQuery(string SqlString)
        {
            using (IDbConnection iConn = this.GetConnection())
            {
                using (IDbCommand iCmd = GetCommand(SqlString, iConn))
                {
                    iConn.Open();
                    try
                    {
                        int rows = iCmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Exception E)
                    {
                        throw new Exception(E.Message);
                    }
                    finally
                    {
                        if (iConn.State != ConnectionState.Closed)
                        {
                            iConn.Close();
                        }
                    }
                }
            }
        }
        /// <summary>  
        /// ִ�ж���SQL��䣬ʵ�����ݿ�����  
        /// </summary>  
        /// <param name="SQLStringList">����SQL���</param>          
        public void ExecuteNonQuery(ArrayList SQLStringList)
        {
            //using��Ϊ��䣬���ڶ���һ����Χ���ڴ˷�Χ��ĩβ���ͷŶ���  
            using (IDbConnection iConn = this.GetConnection())
            {
                iConn.Open();
                using (IDbCommand iCmd = GetCommand())
                {
                    iCmd.Connection = iConn;
                    using (System.Data.IDbTransaction iDbTran = iConn.BeginTransaction())
                    {
                        iCmd.Transaction = iDbTran;
                        try
                        {
                            for (int n = 0; n < SQLStringList.Count; n++)
                            {
                                string strsql = SQLStringList[n].ToString();
                                if (strsql.Trim().Length > 1)
                                {
                                    iCmd.CommandText = strsql;
                                    iCmd.ExecuteNonQuery();
                                }
                            }
                            iDbTran.Commit();
                        }
                        catch (System.Exception E)
                        {
                            iDbTran.Rollback();
                            throw new Exception(E.Message);
                        }
                        finally
                        {
                            if (iConn.State != ConnectionState.Closed)
                            {
                                iConn.Close();
                            }
                        }
                    }
                }
            }
        }
        /// <summary>  
        /// ִ�д�һ���洢���̲����ĵ�SQL��䡣  
        /// </summary>  
        /// <param name="SQLString">SQL���</param>  
        /// <param name="content">��������,����һ���ֶ��Ǹ�ʽ���ӵ����£���������ţ�����ͨ�������ʽ���</param>  
        /// <returns>Ӱ��ļ�¼��</returns>  
        public int ExecuteNonQuery(string SqlString, string content)
        {
            using (IDbConnection iConn = this.GetConnection())
            {
                using (IDbCommand iCmd = GetCommand(SqlString, iConn))
                {
                    IDataParameter myParameter = this.iDbPara("@content", "Text");
                    myParameter.Value = content;
                    iCmd.Parameters.Add(myParameter);
                    iConn.Open();
                    try
                    {
                        int rows = iCmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        if (iConn.State != ConnectionState.Closed)
                        {
                            iConn.Close();
                        }
                    }
                }
            }
        }
        /// <summary>  
        /// �����ݿ������ͼ���ʽ���ֶ�(������������Ƶ���һ��ʵ��)  
        /// </summary>  
        /// <param name="strSQL">SQL���</param>  
        /// <param name="fs">ͼ���ֽ�,���ݿ���ֶ�����Ϊimage�����</param>  
        /// <returns>Ӱ��ļ�¼��</returns>  
        public int ExecuteNonQueryInsertImg(string SqlString, byte[] fs)
        {
            using (IDbConnection iConn = this.GetConnection())
            {
                using (IDbCommand iCmd = GetCommand(SqlString, iConn))
                {
                    IDataParameter myParameter = this.iDbPara("@content", "Image");
                    myParameter.Value = fs;
                    iCmd.Parameters.Add(myParameter);
                    iConn.Open();
                    try
                    {
                        int rows = iCmd.ExecuteNonQuery();
                        return rows;
                    }
                    catch (System.Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        if (iConn.State != ConnectionState.Closed)
                        {
                            iConn.Close();
                        }
                    }
                }
            }
        }
        public object ExecuteScalar(IDbCommand cmd)
        {
            using (IDbConnection iConn = GetConnection())
            {
                iConn.Open();
                try
                {
                    cmd.Connection = iConn;
                    object obj = cmd.ExecuteScalar();
                    if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                    {
                        return null;
                    }
                    else
                    {
                        return obj;
                    }
                }
                catch (System.Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    if (iConn.State != ConnectionState.Closed)
                    {
                        iConn.Close();
                    }
                }
            }
        }
        /// <summary>  
        /// ִ��һ�������ѯ�����䣬���ز�ѯ�����object����  
        /// </summary>  
        /// <param name="SQLString">�����ѯ������</param>  
        /// <returns>��ѯ�����object��</returns>  
        public object ExecuteScalar(string SqlString)
        {
            using (IDbConnection iConn = GetConnection())
            {
                using (IDbCommand iCmd = GetCommand(SqlString, iConn))
                {
                    iConn.Open();
                    try
                    {
                        object obj = iCmd.ExecuteScalar();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        if (iConn.State != ConnectionState.Closed)
                        {
                            iConn.Close();
                        }
                    }
                }
            }
        }

        /// <summary>  
        /// ִ�в�ѯ��䣬����DataSet  
        /// </summary>  
        /// <param name="SQLString">��ѯ���</param>  
        /// <returns>DataSet</returns>  
        public DataSet ExecuteDataSet(string sqlString)
        {
            using (IDbConnection iConn = this.GetConnection())
            {
                using (IDbCommand iCmd = GetCommand(sqlString, iConn))
                {
                    DataSet ds = new DataSet();
                    iConn.Open();
                    try
                    {
                        IDataAdapter iAdapter = this.GetAdapater(sqlString, iConn);
                        iAdapter.Fill(ds);
                        return ds;
                    }
                    catch (System.Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        if (iConn.State != ConnectionState.Closed)
                        {
                            iConn.Close();
                        }
                    }
                }
            }
        }
        /// <summary>  
        /// ִ�в�ѯ��䣬����DataSet  
        /// </summary>  
        /// <param name="sqlString">��ѯ���</param>  
        /// <param name="dataSet">Ҫ����DataSet</param>  
        /// <param name="tableName">Ҫ���ı���</param>  
        /// <returns>DataSet</returns>  
        public DataSet ExecuteDataSet(string sqlString, DataSet dataSet, string tableName)
        {
            using (IDbConnection iConn = this.GetConnection())
            {
                using (IDbCommand iCmd = GetCommand(sqlString, iConn))
                {
                    iConn.Open();
                    try
                    {
                        IDataAdapter iAdapter = this.GetAdapater(sqlString, iConn);

                        ((SqlDataAdapter)iAdapter).Fill(dataSet, tableName);
                        return dataSet;
                    }
                    catch (System.Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        if (iConn.State != ConnectionState.Closed)
                        {
                            iConn.Close();
                        }
                    }
                }
            }
        }
        /// <summary>  
        /// ִ��SQL��� ���ش洢����  
        /// </summary>  
        /// <param name="sqlString">Sql���</param>  
        /// <param name="dataSet">Ҫ����DataSet</param>  
        /// <param name="startIndex">��ʼ��¼</param>  
        /// <param name="pageSize">ҳ���¼��С</param>  
        /// <param name="tableName">������</param>  
        /// <returns>DataSet</returns>  
        public DataSet ExecuteDataSet(string sqlString, DataSet dataSet, int startIndex, int pageSize, string tableName)
        {
            using (IDbConnection iConn = this.GetConnection())
            {
                iConn.Open();
                try
                {
                    IDataAdapter iAdapter = this.GetAdapater(sqlString, iConn);

                    ((SqlDataAdapter)iAdapter).Fill(dataSet, startIndex, pageSize, tableName);

                    return dataSet;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                finally
                {
                    if (iConn.State != ConnectionState.Closed)
                    {
                        iConn.Close();
                    }
                }
            }
        }
        /// <summary>  
        /// ִ�в�ѯ��䣬��XML�ļ�д������  
        /// </summary>  
        /// <param name="sqlString">��ѯ���</param>  
        /// <param name="xmlPath">XML�ļ�·��</param>  
        public void WriteToXml(string sqlString, string xmlPath)
        {
            ExecuteDataSet(sqlString).WriteXml(xmlPath);
        }
        /// <summary>  
        /// ִ�в�ѯ���  
        /// </summary>  
        /// <param name="SqlString">��ѯ���</param>  
        /// <returns>DataTable </returns>  
        public DataTable ExecuteDataTable(string sqlString)
        {
            using (IDbConnection iConn = this.GetConnection())
            {
                //IDbCommand iCmd  =  GetCommand(sqlString,iConn);  
                DataSet ds = new DataSet();
                try
                {
                    IDataAdapter iAdapter = this.GetAdapater(sqlString, iConn);
                    iAdapter.Fill(ds);
                }
                catch (System.Exception e)
                {
                    throw new Exception(e.Message);
                }
                finally
                {
                    if (iConn.State != ConnectionState.Closed)
                    {
                        iConn.Close();
                    }
                }
                return ds.Tables[0];
            }
        }
        /// <summary>  
        /// ִ�в�ѯ���  
        /// </summary>  
        /// <param name="SqlString">��ѯ���</param>  
        /// <returns>DataTable </returns>  
        public DataTable ExecuteDataTable(string SqlString, string Proc)
        {
            using (IDbConnection iConn = this.GetConnection())
            {
                using (IDbCommand iCmd = GetCommand(SqlString, iConn))
                {
                    iCmd.CommandType = CommandType.StoredProcedure;
                    DataSet ds = new DataSet();
                    try
                    {
                        IDataAdapter iDataAdapter = this.GetAdapater(SqlString, iConn);
                        iDataAdapter.Fill(ds);
                    }
                    catch (System.Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        if (iConn.State != ConnectionState.Closed)
                        {
                            iConn.Close();
                        }
                    }
                    return ds.Tables[0];
                }
            }
        }
        /// <summary>  
        /// ִ�в�ѯ������DataView���ؽ����   
        /// </summary>  
        /// <param name="Sql">SQL���</param>  
        /// <returns>DataView</returns>  
        public DataView ExecuteDataView(string Sql)
        {
            using (IDbConnection iConn = this.GetConnection())
            {
                using (IDbCommand iCmd = GetCommand(Sql, iConn))
                {
                    DataSet ds = new DataSet();
                    try
                    {
                        IDataAdapter iDataAdapter = this.GetAdapater(Sql, iConn);
                        iDataAdapter.Fill(ds);
                        return ds.Tables[0].DefaultView;
                    }
                    catch (System.Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        if (iConn.State != ConnectionState.Closed)
                        {
                            iConn.Close();
                        }
                    }
                }
            }
        }
        #endregion
        #region ִ�д�������SQL���  
        /// <summary>  
        /// ִ��SQL��䣬����Ӱ��ļ�¼��  
        /// </summary>  
        /// <param name="SQLString">SQL���</param>  
        /// <returns>Ӱ��ļ�¼��</returns>  
        public int ExecuteNonQuery(string SQLString, params IDataParameter[] iParms)
        {
            using (IDbConnection iConn = this.GetConnection())
            {
                IDbCommand iCmd = GetCommand();
                {
                    try
                    {
                        PrepareCommand(out iCmd, iConn, null, SQLString, iParms);
                        int rows = iCmd.ExecuteNonQuery();
                        iCmd.Parameters.Clear();
                        return rows;
                    }
                    catch (System.Exception E)
                    {
                        throw new Exception(E.Message);
                    }
                    finally
                    {
                        iCmd.Dispose();
                        if (iConn.State != ConnectionState.Closed)
                        {
                            iConn.Close();
                        }
                    }
                }
            }
        }
        /// <summary>  
        /// ִ�ж���SQL��䣬ʵ�����ݿ�����  
        /// </summary>  
        /// <param name="SQLStringList">SQL���Ĺ�ϣ��keyΪsql��䣬value�Ǹ�����SqlParameter[]��</param>  
        public void ExecuteNonQueryTran(Hashtable SQLStringList)
        {
            using (IDbConnection iConn = this.GetConnection())
            {
                iConn.Open();
                using (IDbTransaction iTrans = iConn.BeginTransaction())
                {
                    IDbCommand iCmd = GetCommand();
                    try
                    {
                        //ѭ��  
                        foreach (DictionaryEntry myDE in SQLStringList)
                        {
                            string cmdText = myDE.Key.ToString();
                            IDataParameter[] iParms = (IDataParameter[])myDE.Value;
                            PrepareCommand(out iCmd, iConn, iTrans, cmdText, iParms);
                            int val = iCmd.ExecuteNonQuery();
                            iCmd.Parameters.Clear();
                        }
                        iTrans.Commit();
                    }
                    catch
                    {
                        iTrans.Rollback();
                        throw;
                    }
                    finally
                    {
                        iCmd.Dispose();
                        if (iConn.State != ConnectionState.Closed)
                        {
                            iConn.Close();
                        }
                    }
                }
            }
        }
        /// <summary>  
        /// ִ��һ�������ѯ�����䣬���ز�ѯ�����object����  
        /// </summary>  
        /// <param name="SQLString">�����ѯ������</param>  
        /// <returns>��ѯ�����object��</returns>  
        public object ExecuteScalar(string SQLString, params IDataParameter[] iParms)
        {
            using (IDbConnection iConn = this.GetConnection())
            {
                IDbCommand iCmd = GetCommand();
                {
                    try
                    {
                        PrepareCommand(out iCmd, iConn, null, SQLString, iParms);
                        object obj = iCmd.ExecuteScalar();
                        iCmd.Parameters.Clear();
                        if ((Object.Equals(obj, null)) || (Object.Equals(obj, System.DBNull.Value)))
                        {
                            return null;
                        }
                        else
                        {
                            return obj;
                        }
                    }
                    catch (System.Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                    finally
                    {
                        iCmd.Dispose();
                        if (iConn.State != ConnectionState.Closed)
                        {
                            iConn.Close();
                        }
                    }
                }
            }
        }
        /// <summary>  
        /// ִ�в�ѯ��䣬����IDataAdapter  
        /// </summary>  
        /// <param name="strSQL">��ѯ���</param>  
        /// <returns>IDataAdapter</returns>  
        public IDataReader ExecuteReader(string strSQL)
        {
            IDbConnection iConn = this.GetConnection();
            {
                IDbCommand iCmd = GetCommand();
                {
                    try
                    {
                        PrepareCommand(out iCmd, iConn, null, strSQL, null);
                        System.Data.IDataReader iReader = iCmd.ExecuteReader();
                        iCmd.Parameters.Clear();
                        return iReader;
                    }
                    catch (System.Exception e)
                    {
                        iCmd.Dispose();
                        if (iConn.State != ConnectionState.Closed)
                        {
                            iConn.Close();
                        }
                        throw new Exception(e.Message);
                    }
                    finally
                    {


                    }
                }
            }
        }
        /// <summary>  
        /// ִ�в�ѯ��䣬����IDataReader  
        /// </summary>  
        /// <param name="strSQL">��ѯ���</param>  
        /// <returns> IDataReader </returns>  
        public IDataReader ExecuteReader(string SQLString, params IDataParameter[] iParms)
        {
            IDbConnection iConn = this.GetConnection();
            {
                IDbCommand iCmd = GetCommand();
                {
                    try
                    {
                        PrepareCommand(out iCmd, iConn, null, SQLString, iParms);
                        System.Data.IDataReader iReader = iCmd.ExecuteReader();
                        iCmd.Parameters.Clear();
                        return iReader;
                    }
                    catch (System.Exception e)
                    {
                        iCmd.Dispose();
                        if (iConn.State != ConnectionState.Closed)
                        {
                            iConn.Close();
                        }
                        throw new Exception(e.Message);
                    }
                    finally
                    {

                    }
                }
            }
        }
        /// <summary>  
        /// ִ�в�ѯ��䣬����DataSet  
        /// </summary>  
        /// <param name="SQLString">��ѯ���</param>  
        /// <returns>DataSet</returns>  
        public DataSet ExecuteDataSet(string sqlString, CommandType cmdType, params IDataParameter[] iParms)
        {
            using (IDbConnection iConn = this.GetConnection())
            {
                IDbCommand iCmd = GetCommand();
                {
                    PrepareCommand(out iCmd, iConn, null, sqlString, iParms);
                    iCmd.CommandType = cmdType;
                    try
                    {
                        IDataAdapter iAdapter = this.GetAdapater(iCmd, iConn);
                        DataSet ds = new DataSet();
                        iAdapter.Fill(ds);
                        iCmd.Parameters.Clear();
                        return ds;
                    }
                    catch (System.Exception ex)
                    {
                        throw new Exception(ex.Message);
                    }
                    finally
                    {
                        iCmd.Dispose();
                        if (iConn.State != ConnectionState.Closed)
                        {
                            iConn.Close();
                        }
                    }
                }
            }
        }
        /// <summary>  
        /// ��ʼ��Command  
        /// </summary>  
        /// <param name="iCmd"></param>  
        /// <param name="iConn"></param>  
        /// <param name="iTrans"></param>  
        /// <param name="cmdText"></param>  
        /// <param name="iParms"></param>  
        private void PrepareCommand(out IDbCommand iCmd, IDbConnection iConn, System.Data.IDbTransaction iTrans, string cmdText, IDataParameter[] iParms)
        {
            if (iConn.State != ConnectionState.Open)
                iConn.Open();
            iCmd = this.GetCommand();
            iCmd.Connection = iConn;
            iCmd.CommandText = cmdText;
            if (iTrans != null)
                iCmd.Transaction = iTrans;
            iCmd.CommandType = CommandType.Text;//cmdType;  
            if (iParms != null)
            {
                foreach (IDataParameter parm in iParms)
                    iCmd.Parameters.Add(parm);
            }
        }
        #endregion
        #region �洢���̲���  
        /// <summary>  
        /// ִ�д洢����  
        /// </summary>  
        /// <param name="storedProcName">�洢������</param>  
        /// <param name="parameters">�洢���̲���</param>  
        /// <returns>SqlDataReader</returns>  
        public SqlDataReader RunProcedure(string storedProcName, IDataParameter[] parameters)
        {
            IDbConnection iConn = this.GetConnection();
            {
                iConn.Open();

                using (SqlCommand sqlCmd = BuildQueryCommand(iConn, storedProcName, parameters))
                {
                    return sqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
                }
            }
        }
        /// <summary>  
        /// ִ�д洢����  
        /// </summary>  
        /// <param name="storedProcName">�洢������</param>  
        /// <param name="parameters">�洢���̲���</param>  
        /// <param name="tableName">DataSet����еı���</param>  
        /// <returns>DataSet</returns>  
        public DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, string tableName)
        {
            using (IDbConnection iConn = this.GetConnection())
            {
                DataSet dataSet = new DataSet();
                iConn.Open();
                IDataAdapter iDA = this.GetAdapater();
                iDA = this.GetAdapater(BuildQueryCommand(iConn, storedProcName, parameters));
                ((SqlDataAdapter)iDA).Fill(dataSet, tableName);
                if (iConn.State != ConnectionState.Closed)
                {
                    iConn.Close();
                }
                return dataSet;
            }
        }
        /// <summary>  
        /// ִ�д洢����  
        /// </summary>  
        /// <param name="storedProcName">�洢������</param>  
        /// <param name="parameters">�洢���̲���</param>  
        /// <param name="tableName">DataSet����еı���</param>  
        /// <param name="startIndex">��ʼ��¼����</param>  
        /// <param name="pageSize">ҳ���¼��С</param>  
        /// <returns>DataSet</returns>  
        public DataSet RunProcedure(string storedProcName, IDataParameter[] parameters, int startIndex, int pageSize, string tableName)
        {
            using (IDbConnection iConn = this.GetConnection())
            {
                DataSet dataSet = new DataSet();
                iConn.Open();
                IDataAdapter iDA = this.GetAdapater();
                iDA = this.GetAdapater(BuildQueryCommand(iConn, storedProcName, parameters));

                ((SqlDataAdapter)iDA).Fill(dataSet, startIndex, pageSize, tableName);
                if (iConn.State != ConnectionState.Closed)
                {
                    iConn.Close();
                }
                return dataSet;
            }
        }
        /// <summary>  
        /// ִ�д洢���� ����Ѿ����ڵ�DataSet���ݼ�   
        /// </summary>  
        /// <param name="storeProcName">�洢��������</param>  
        /// <param name="parameters">�洢���̲���</param>  
        /// <param name="dataSet">Ҫ�������ݼ�</param>  
        /// <param name="tablename">Ҫ���ı���</param>  
        /// <returns></returns>  
        public DataSet RunProcedure(string storeProcName, IDataParameter[] parameters, DataSet dataSet, string tableName)
        {
            using (IDbConnection iConn = this.GetConnection())
            {
                iConn.Open();
                IDataAdapter iDA = this.GetAdapater();
                iDA = this.GetAdapater(BuildQueryCommand(iConn, storeProcName, parameters));

                ((SqlDataAdapter)iDA).Fill(dataSet, tableName);

                if (iConn.State != ConnectionState.Closed)
                {
                    iConn.Close();
                }
                return dataSet;
            }
        }
        /// <summary>  
        /// ִ�д洢���̲�������Ӱ�������  
        /// </summary>  
        /// <param name="storedProcName"></param>  
        /// <param name="parameters"></param>  
        /// <returns></returns>  
        public int RunProcedureNoQuery(string storedProcName, IDataParameter[] parameters)
        {
            int result = 0;
            using (IDbConnection iConn = this.GetConnection())
            {
                iConn.Open();
                using (SqlCommand scmd = BuildQueryCommand(iConn, storedProcName, parameters))
                {
                    result = scmd.ExecuteNonQuery();
                }

                if (iConn.State != ConnectionState.Closed)
                {
                    iConn.Close();
                }
            }
            return result;
        }
        public string RunProcedureExecuteScalar(string storeProcName, IDataParameter[] parameters)
        {
            string result = string.Empty;
            using (IDbConnection iConn = this.GetConnection())
            {
                iConn.Open();
                using (SqlCommand scmd = BuildQueryCommand(iConn, storeProcName, parameters))
                {
                    object obj = scmd.ExecuteScalar();
                    if (obj == null)
                        result = null;
                    else
                        result = obj.ToString();
                }
                if (iConn.State != ConnectionState.Closed)
                {
                    iConn.Close();
                }
            }
            return result;
        }
        /// <summary>  
        /// ���� SqlCommand ����(��������һ���������������һ������ֵ)  
        /// </summary>  
        /// <param name="connection">���ݿ�����</param>  
        /// <param name="storedProcName">�洢������</param>  
        /// <param name="parameters">�洢���̲���</param>  
        /// <returns>SqlCommand</returns>  
        private SqlCommand BuildQueryCommand(IDbConnection iConn, string storedProcName, IDataParameter[] parameters)
        {
            IDbCommand iCmd = GetCommand(storedProcName, iConn);
            iCmd.CommandType = CommandType.StoredProcedure;
            if (parameters == null)
            {
                return (SqlCommand)iCmd;
            }
            foreach (IDataParameter parameter in parameters)
            {
                iCmd.Parameters.Add(parameter);
            }
            return (SqlCommand)iCmd;
        }

        #endregion
    }
}
