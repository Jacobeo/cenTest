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

        public async Task AddPrimarySalesperson(int districtId, int salespersonId, CancellationToken cancellationToken)
        {
            var sql = "UPDATE District SET PrimarySalespersonId = @salespersonId WHERE Id = @districtId";
            var cmd = new CommandDefinition(sql, new { districtId, salespersonId }, cancellationToken: cancellationToken);
            await _connectionFactory.CreateConnection().ExecuteAsync(cmd);
        }

        public async Task AddSecondarySalesperson(int districtId, int salespersonId,
            CancellationToken cancellationToken)
        {
            var sql =
                @"IF NOT EXISTS (SELECT 1 FROM SalespersonDistrict WHERE SalespersonId = @salespersonId AND DistrictId = @districtId) " +
                "OR NOT EXISTS (SELECT 1 FROM District WHERE Id = @districtId AND PrimarySalesPersonId = @salespersonId) " +
                "BEGIN " +
                "INSERT INTO SalespersonDistrict (SalespersonId, DistrictId) VALUES (@salespersonId, @districtId)" +
                "END";

            var cmd = new CommandDefinition(sql, new { districtId, salespersonId },
                cancellationToken: cancellationToken);
            await _connectionFactory.CreateConnection().ExecuteAsync(cmd);
        }

        public async Task DeleteSalesperson(int districtId, int salesPersonId, CancellationToken cancellationToken)
        {
            var sql =
                @"DELETE FROM SalespersonDistrict WHERE salesPersonId = @salesPersonId and DistrictId = @districtId";

            var cmd = new CommandDefinition(sql, new { districtId, salesPersonId },
                cancellationToken: cancellationToken);
            await _connectionFactory.CreateConnection().ExecuteAsync(cmd);
        }

        public virtual async Task<IEnumerable<IDistrict>> GetAllAsync(CancellationToken cancellationToken)
        {
            var sql = "SELECT * FROM District";

            var cmd = new CommandDefinition(sql, cancellationToken: cancellationToken);
            return await _connectionFactory.CreateConnection().QueryAsync<District>(cmd);
        }
    }
}
