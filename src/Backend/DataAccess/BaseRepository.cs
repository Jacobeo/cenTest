using DataAccess.Repositories.Interfaces;

namespace DataAccess
{
    public abstract class BaseRepository
    {
        protected readonly IDbConnectionFactory _connectionFactory;

        public BaseRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
    }
}
