using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nzh.Allen.Repository.DBHeper
{
    public class DbContext
    {
        public IConfiguration configuration { set; get; }
        public System.Data.IDbConnection GetConnection()
        {
            string connectionString = configuration.GetValue<string>("Db:ConnectionString");
#if MYSQL
            var connection = new MySqlConnection(connectionString);
            connection.Open();
            return connection;
#endif
#if ORACLE
            var connection = new OracleConnection(connectionString);
            connection.Open();
            return connection;
#endif
#if SQLSERVER
            var connection = new SqlConnection(connectionString);
            connection.Open();
            return connection;
#endif
            throw new Exception("数据库类型错误");
        }

    }
}
