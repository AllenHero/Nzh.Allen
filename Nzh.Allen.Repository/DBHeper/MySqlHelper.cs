using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.Repository
{
    public class MySqlHelper
    {
        //public static string mysqlconnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["mysqlconn"].ConnectionString;

        public IConfiguration configuration { set; get; }

        public System.Data.IDbConnection GetConnection()
        {
            string mysqlconnectionString = configuration.GetValue<string>("Db:ConnectionString");
            var connection = new MySqlConnection(mysqlconnectionString);
            connection.Open();
            return connection;
        }
    }
}
