using System.Net.Sockets;
using Backend.Core.Models;
using Backend.DataAccess;
using Backend.DataAccess.Models;
using Backend.DataAccess.Repositories.Interfaces;
using Dapper;

namespace Backend.DataAccess.Repositories
{
    public class SalespersonRepository : BaseRepository<Salesperson>, ISalespersonRepository
    {
        public SalespersonRepository(IDbConnectionFactory connectionFactory, IDatabaseProvider databaseProvider) : base(connectionFactory, databaseProvider)
        {
        }

        public async Task<IEnumerable<ISalesperson>> GetByDistrictId(int districtId,
            CancellationToken cancellationToken)
        {
            var sql = "SELECT S.*, 'Primary' as IsPrimary FROM Salesperson S " +
                      "INNER JOIN District D ON S.Id = D.PrimarySalespersonId " +
                      "WHERE D.Id = @districtId " +
                      "UNION " +
                      "SELECT S.*, 'Secondary' as IsPrimary " +
                      "FROM Salesperson S " +
                      "INNER JOIN SalespersonDistrict SD ON S.Id = SD.SalespersonId " +
                      "INNER JOIN District D ON SD.DistrictId = D.Id " +
                      "WHERE D.Id = @districtId";

            using var connection = _connectionFactory.CreateConnection();
            var salesPersons = await _databaseProvider.QueryAsync<Salesperson>(connection, sql, new { districtId });
            return salesPersons;
        }

        public async Task<IEnumerable<ISalesperson>> GetAllAsync(CancellationToken cancellationToken)
        {
            var sql = "SELECT * FROM Salesperson";

            using var connection = _connectionFactory.CreateConnection();
            var salesPersons = await _databaseProvider.QueryAsync<Salesperson>(connection, sql);
            return salesPersons;
        }
    }
}
