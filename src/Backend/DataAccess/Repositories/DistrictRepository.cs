using Backend.Core.Models;
using Backend.DataAccess.Models;
using Backend.DataAccess.Repositories.Interfaces;
using Dapper;

namespace Backend.DataAccess.Repositories
{
    public class DistrictRepository : BaseRepository, IDistrictRepository
    {
        private readonly ISalespersonRepository _salespersonRepository;
        private readonly IStoreRepository _storeRepository;

        public DistrictRepository(IDbConnectionFactory connectionFactory, IStoreRepository storeRepository,
            ISalespersonRepository salespersonRepository) : base(connectionFactory)
        {
            _storeRepository = storeRepository;
            _salespersonRepository = salespersonRepository;
        }

        public virtual async Task<IEnumerable<IDistrict>> GetAllAsync(CancellationToken cancellationToken)
        {
            var sql = "SELECT * FROM District";

            var cmd = new CommandDefinition(sql, cancellationToken: cancellationToken);
            return await _connectionFactory.CreateConnection().QueryAsync<District>(cmd);
        }
    }
}
