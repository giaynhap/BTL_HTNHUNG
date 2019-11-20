using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BqlMQtt.Database
{
    class DBSQLServerUtils
    {

        public static SqlConnection
        GetDBConnection(string datasource, string database, string username, string password)
        {
 
            string connString = @"Data Source=" + datasource + ";Initial Catalog="
                        + database + ";Persist Security Info=True;User ID=" + username + ";Password=" + password;

            SqlConnection conn = new SqlConnection(connString);

            return conn;
        }
        public static SqlConnection GetDBConnection()
        {
            string datasource = @".\SQLEXPRESS";

            string database = "test";
            string username = "giaynhap";
            string password = "123456";

            return DBSQLServerUtils.GetDBConnection(datasource, database, username, password);
        }

        public static bool StoreDht11(double temp,double humi)
        {
            try
            {
                SqlConnection conn = DBSQLServerUtils.GetDBConnection();
                SqlCommand cmd = new SqlCommand("insert into dht11_data (temp,humi) values (@temp,@humi)", conn);
                cmd.Parameters.AddWithValue("@temp", temp);
                cmd.Parameters.AddWithValue("@humi", humi);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }

    }
}
