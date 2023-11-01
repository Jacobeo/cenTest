using Backend.Core.Models;
using Backend.DataAccess.Models;
using Backend.DataAccess.Repositories.Interfaces;
using Dapper;

namespace Backend.DataAccess.Repositories
{
    public class DistrictRepository : BaseRepository<District>, IDistrictRepository
    {
        public DistrictRepository(IDbConnectionFactory connectionFactory, IDatabaseProvider databaseProvider) : base(connectionFactory, databaseProvider)
        {
        }

        public async Task AddPrimarySalesperson(int districtId, int salespersonId, CancellationToken cancellationToken)
        {
            var sql = "UPDATE District SET PrimarySalespersonId = @salespersonId WHERE Id = @districtId";
            using var connection = _connectionFactory.CreateConnection();
            await _databaseProvider.ExecuteAsync(connection, sql, new { districtId, salespersonId }, cancellationToken: cancellationToken);
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

            using var connection = _connectionFactory.CreateConnection();
            await _databaseProvider.ExecuteAsync(connection, sql, new { districtId, salespersonId }, cancellationToken: cancellationToken);
        }

        public async Task DeleteSalesperson(int districtId, int salesPersonId, CancellationToken cancellationToken)
        {
            var sql =
                @"DELETE FROM SalespersonDistrict WHERE salesPersonId = @salesPersonId and DistrictId = @districtId";

            using var connection = _connectionFactory.CreateConnection();
            await _databaseProvider.ExecuteAsync(connection, sql, new { districtId, salesPersonId }, cancellationToken: cancellationToken);
        }

        public virtual async Task<IEnumerable<IDistrict>> GetAllAsync(CancellationToken cancellationToken)
        {
            var sql = "SELECT * FROM District";

            using var connection = _connectionFactory.CreateConnection();
            var districts = await _databaseProvider.QueryAsync<District>(connection, sql);
            return districts;
        }
    }
}
