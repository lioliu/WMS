using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace WMS
{
    class DBUtility
    {
        private static string connect_string = @"server=DESKTOP-6UC4H8P\SQLEXPRESS;database=WMS;integrated security=SSPI";
        private static string power_connect_string  = @"server=DESKTOP-6UC4H8P\SQLEXPRESS;database=WMS;integrated security=SSPI";
        #region Execute sql
        /// <summary>
        /// Execute SQL statement 
        /// </summary>
        /// <param name="sql">Statement to be executed</param>
        /// <returns> affected rows</returns>
        public static int ExecuteSQL(string sql)
        {
            int count = 0;
            SqlConnection con = new SqlConnection(power_connect_string);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                //cmd.Connection = con;
                //cmd.CommandText = sql;
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                //mod here
                LogMessage(error.Message, "log-error");
                throw;
            }
            finally
            {
                con.Close();
                con.Dispose();
            }
            return count;
        }
        /// <summary>
        /// Execute SQL statement
        /// </summary>
        /// <param name="sql_list">the list of Statements to be executed</param>
        /// <returns> affected rows</returns>
        public static int ExecuteSQL(List<string> sql_list)
        {
            int count = 0;
            SqlConnection con = new SqlConnection(power_connect_string);
            con.Open();
            SqlCommand cmd = new SqlCommand()
            {
                Connection = con
            };
            //create the transaction
            SqlTransaction transaction = con.BeginTransaction();
            cmd.Transaction = transaction;
            try
            {
                foreach (string sql in sql_list)
                {
                    cmd.CommandText = sql;
                    count += cmd.ExecuteNonQuery();
                }
                transaction.Commit();
            }
            catch (Exception error)
            {
                transaction.Rollback();
                Console.Write(error);
                //LogMessage(error.Message, "log-error");
                throw;
            }
            finally
            {
                con.Close();
                con.Dispose();
                transaction.Dispose();
                cmd.Dispose();
            }
            return count;
        }
        #endregion
        #region get data
        /// <summary>
        /// get the data by execute the sql
        /// </summary>
        /// <param name="sql">query statement</param>
        /// <returns>result data</returns>
        public static DataTable GetData(string sql)
        {
           
            DataTable dataTable = new DataTable();
            SqlDataAdapter oda = null;
            try
            {
                oda = new SqlDataAdapter(sql, connect_string);
                oda.Fill(dataTable);
                oda.Dispose();

            }
            catch (Exception error)
            {
                LogMessage(error.Message, "log-error");
                throw;
            }
            finally
            {
                oda.Dispose();
                dataTable.Dispose();
            }
            return dataTable;
        }

        /// <summary>
        /// Execute SQL statement 
        /// </summary>
        /// <param name="list_sql">the list of the statements to be execute</param>
        /// <returns>result data</returns>
        public static DataSet GetData(List<string> list_sql)
        {
            DataSet data_set = new DataSet();
            DataTable data_table = new DataTable();
            SqlDataAdapter oda = null;
            SqlConnection con = new SqlConnection(connect_string);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                foreach (string sql in list_sql)
                {
                    cmd.CommandText = sql;
                    oda = new SqlDataAdapter(cmd);
                    oda.Fill(data_table);
                    data_set.Tables.Add(data_table);
                    data_table = new DataTable();
                }
            }
            catch (Exception error)
            {
                LogMessage(error.Message, "log-error");
                throw;
            }
            finally
            {
                con.Dispose();
                oda.Dispose();
                data_table.Dispose();
            }

            return data_set;
        }

        #endregion
        #region execute procedure

        /// <summary>
        /// execute the procedure to get the data format as Sql parameter
        /// </summary>
        /// <param name="procedure_name">procedure to be execute</param>
        /// <param name="parms">Sql parameters</param>
        /// <returns>result parameters</returns>
        public static SqlParameter[] ExecuteProcedure(string procedure_name, SqlParameter[] parms)
        {
            DataTable data_table = new DataTable();
            SqlConnection con = new SqlConnection(connect_string);
            SqlCommand cmd = new SqlCommand();
            DataSet data_set = new DataSet();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procedure_name;
            try
            {
                con.Open();
                cmd.Connection = con;
                foreach (SqlParameter parm in parms)
                {
                    cmd.Parameters.Add(parm);
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception error)
            {
                LogMessage(procedure_name + " : " + error.Message, "log-error");
                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Dispose();
                con.Dispose();
            }
            return parms;
        }

        /// <summary>
        /// execute procedure get the data format as dataset
        /// </summary>
        /// <param name="procedure_name">procedure to be execute</param>
        /// <param name="parms">pracle parameters</param>
        /// <param name="data_set">the data set to get the data</param>
        public static void ExecuteProcedure(string procedure_name, SqlParameter[] parms, out DataSet data_set)
        {
            DataTable data_table = new DataTable();
            SqlDataAdapter oda = new SqlDataAdapter();
            SqlConnection con = new SqlConnection();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = procedure_name;
            data_set = new DataSet();
            try
            {
                con.Open();
                cmd.Connection = con;
                foreach (SqlParameter parm in parms)
                {
                    cmd.Parameters.Add(parm);
                }
                oda.SelectCommand = cmd;
                oda.Fill(data_table);
                data_set.Tables.Add(data_table);
            }
            catch (Exception error)
            {
                LogMessage(procedure_name + " : " + error.Message, "log-error");
                throw;
            }
            finally
            {
                if (con.State == ConnectionState.Open)
                    con.Close();
                con.Dispose();
                cmd.Parameters.Clear();
                cmd.Dispose();
                oda.Dispose();
            }
        }

        #endregion

        /// <summary>
        /// write log message
        /// </summary>
        /// <param name="outPutStr">log infor</param>
        /// <param name="type">log type</param>
        public static void LogMessage(string outPutStr, string type)
        {
        }

        


    }
}
