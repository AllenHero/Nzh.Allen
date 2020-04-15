using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Nzh.Allen.Repository
{
    public class SqlHelper
    {
        public IConfiguration configuration { set; get; }

        public System.Data.IDbConnection GetConnection()
        {
            string connectionString = configuration.GetValue<string>("Db:ConnectionString");
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
        }
    }
}
