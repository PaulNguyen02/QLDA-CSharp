using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace WindowsFormsApp1.DAO
{
    class Connection
    {
        private static String connectionString = "server=.; database=QLHT;integrated security=true";
        public static SqlConnection GetDBConnection()
        {
            SqlConnection conn = new SqlConnection(connectionString);
            return conn;
        }
    }
}
