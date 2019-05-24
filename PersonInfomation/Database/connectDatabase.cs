using System.Data;
using System.Data.SqlClient;

namespace PersonInfomation.Database
{
    class connectDatabase
    {
        public static string ConnectionString = @"Data Source=.;Initial Catalog=Person;Integrated Security=True";
        public static SqlConnection Conn;
        public static SqlDataAdapter da;
        public static DataTable dataTable;
        public static DataSet ds;

        //Check status Connection
        public static SqlConnection OpenConnection()
        {
            Conn = new SqlConnection(ConnectionString);
            if (Conn.State == ConnectionState.Closed)
                Conn.Open();
            return Conn;
        }

        //Get value from Database when use Select
        public static DataTable Excute(string sql)
        {
            SqlConnection conn = OpenConnection();
            da = new SqlDataAdapter(sql, conn);
            ds = new DataSet();
            da.Fill(ds);
            dataTable = ds.Tables[0];
            return dataTable;
        }

        //Excute query when use Insert - Update - Delete
        public static void ExecuteNonQuery(string sql)
        {
            SqlConnection conn = OpenConnection();
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }
}
