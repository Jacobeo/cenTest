using System.Data;
using System.Data.SqlClient;
using Backend.DataAccess.Repositories.Interfaces;

namespace Backend.DataAccess
{
    public class DbConnectionFactory : IDbConnectionFactory
    {
        private readonly string _connectionString;

        public DbConnectionFactory(string connectionString)
        {
            _connectionString = connectionString;
        }

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
