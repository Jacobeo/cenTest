using Backend.DataAccess.Repositories.Interfaces;

namespace Backend.DataAccess
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
