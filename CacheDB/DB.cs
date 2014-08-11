using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CacheDB
{
    public class DB
    {
        public static MySqlConnection myConnection = new MySqlConnection(" Persist Security Info=False;server=localhost;database=cachedb;uid=root;server=localhost;database=cachedb;uid=root;pwd=root");

        public static void CloseConnection()
        {
            myConnection.Close();
            myConnection.Dispose();
        }
    }
}
