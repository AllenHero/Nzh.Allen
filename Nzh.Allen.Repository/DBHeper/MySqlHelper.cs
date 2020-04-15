using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.Repository
{
    public class MySqlHelper
    {
        public IConfiguration configuration { set; get; }

        public System.Data.IDbConnection GetConnection()
        {
            string connectionString = configuration.GetValue<string>("Db:ConnectionString");
            var connection = new MySqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
