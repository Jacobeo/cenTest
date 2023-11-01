using Backend.Core.Models;
using Backend.DataAccess;
using Backend.DataAccess.Models;
using Backend.DataAccess.Repositories.Interfaces;
using Dapper;

namespace Backend.DataAccess.Repositories
{
    public class StoreRepository : BaseRepository<IStore>, IStoreRepository
    {
        public StoreRepository(IDbConnectionFactory connectionFactory, IDatabaseProvider databaseProvider) : base(connectionFactory, databaseProvider)
        {
        }

        public async Task<IEnumerable<IStore>> GetByDistrictId(int districtId, CancellationToken cancellationToken)
        {
            var sql = "SELECT * FROM Store where DistrictId = @districtId";

            using var connection = _connectionFactory.CreateConnection();
            var stores = await _databaseProvider.QueryAsync<Store>(connection, sql, new { districtId });
            return stores;
        }
    }
}
