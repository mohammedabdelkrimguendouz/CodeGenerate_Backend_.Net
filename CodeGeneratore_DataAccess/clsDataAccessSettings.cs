using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGeneratore_DataAccess
{
    static public class clsDataAccessSettings
    {
        private static string _ConnectionString = "";
        public static void SetConnectionString(string Server,string DataBase,string UserId,string Password)
        {
            _ConnectionString = $"Server={Server};Database={DataBase};User Id={UserId};Password={Password}; TrustServerCertificate=true;";
        }
        public static string GetConnectionString()
        {
            return _ConnectionString;
        }
    }
}
