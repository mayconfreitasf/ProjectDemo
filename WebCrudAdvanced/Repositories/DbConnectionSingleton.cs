using System.Data;
using System.Data.SqlClient;

namespace WebCrudAdvanced.Repositories
{
    public class DbConnectionSingleton
    {
        private static IDbConnection _instance;
        private static readonly object Lock = new object();

        private DbConnectionSingleton() { }

        public static IDbConnection GetConnection(string connectionString)
        {
            if (_instance == null)
            {
                lock (Lock)
                {
                    if (_instance == null)
                    {
                        _instance = new SqlConnection(connectionString); 
                    }
                }
            }
            return _instance;
        }
    }
}
