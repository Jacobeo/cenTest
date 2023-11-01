using Backend.DataAccess.Repositories.Interfaces;
using Dapper;

namespace Backend.DataAccess
{
    public abstract class BaseRepository<T> where T : class
    {

        protected readonly IDatabaseProvider _databaseProvider;
        protected readonly IDbConnectionFactory _connectionFactory;

        public BaseRepository(IDbConnectionFactory connectionFactory, IDatabaseProvider databaseProvider)
        {
            _connectionFactory = connectionFactory;
            _databaseProvider = databaseProvider;
        }

    }
}
