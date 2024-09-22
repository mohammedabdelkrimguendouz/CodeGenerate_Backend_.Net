using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneratore_DataAccess
{
    public class clsCodeGeneratoreData
    {
        public static bool IsConnectionValide()
        {
            bool ConnectionValide = false;
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.GetConnectionString()))
                {
                    connection.Open();
                    ConnectionValide=(connection.State == ConnectionState.Open);
                }
            }
            catch (Exception ex)
            {
                ConnectionValide = false;
            }
            return ConnectionValide;
        }

        public static DataTable GetAllTables()
        {
            DataTable dt = new DataTable();
            try
            {
                
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.GetConnectionString()))
                {
                    connection.Open();
                    string Query = @"SELECT TABLE_NAME
                                   FROM INFORMATION_SCHEMA.TABLES
                                    WHERE
                                   TABLE_TYPE = 'BASE TABLE' 
                                   AND TABLE_SCHEMA = 'dbo'
                                   AND TABLE_NAME != 'sysdiagrams';";

                    using (SqlCommand Command = new SqlCommand(Query, connection))
                    {
                        
                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.HasRows)
                                dt.Load(Reader);
                        }
                    }
                   
                }
            }
            catch
            {
                
            }
            return dt;
        }

        public static DataTable GetColumnsInfoForTable(string TableName)
        {
            DataTable dt = new DataTable();
            try
            {
                using (SqlConnection connection = new SqlConnection(clsDataAccessSettings.GetConnectionString()))
                {
                    connection.Open();
                    string Query = @"SELECT  COLUMN_NAME,DATA_TYPE ,IS_NULLABLE  FROM  INFORMATION_SCHEMA.COLUMNS
                                    WHERE 
                                   TABLE_SCHEMA = 'dbo' 
                                 AND TABLE_NAME =@TableName;";

                    using (SqlCommand Command = new SqlCommand(Query, connection))
                    {
                        Command.Parameters.AddWithValue("@TableName", TableName);

                        using (SqlDataReader Reader = Command.ExecuteReader())
                        {
                            if (Reader.HasRows)
                                dt.Load(Reader);
                        }
                    }

                }
            }
            catch
            {

            }
            return dt;

        }
    }
}
