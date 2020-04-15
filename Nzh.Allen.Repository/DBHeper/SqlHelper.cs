using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace Nzh.Allen.Repository.DBHeper
{
    public class SqlHelper
    {
        static string sqlconnectionString = ConfigurationManager.ConnectionStrings["sqlconn"].ToString();

        public static SqlConnection SqlConnection()
        {
            var connection = new SqlConnection(sqlconnectionString);
            connection.Open();
            return connection;
        }
    }
}
