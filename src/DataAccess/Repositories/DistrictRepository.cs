using Core.Models;
using Dapper;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories
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


        public async Task<IDistrictDetails> GetDistrictDetails(int id, CancellationToken cancellationToken)
        {
            var salespersons = await _salespersonRepository.GetByDistrictId(id, cancellationToken);
            var stores = await _storeRepository.GetByDistrictId(id, cancellationToken);

            return new DistrictDetails(stores, salespersons);
        }
    }
}
