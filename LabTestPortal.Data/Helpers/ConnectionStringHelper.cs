using System.Configuration;
using System.Data.SqlClient;

namespace LabTestPortal.Data.Helpers {
    public class ConnectionStringHelper
    {
        private static string ConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["labtestportalConnectionString"].ConnectionString;
        }

        public static void OpenConnection(SqlConnection conn)
        {
            var connectionString = ConnectionString();
            conn.ConnectionString = connectionString;
            conn.Open();
        }
    }
}
