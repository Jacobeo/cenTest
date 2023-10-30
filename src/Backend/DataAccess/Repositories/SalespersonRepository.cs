using Core.Models;
using Dapper;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories
{
    public class SalespersonRepository : BaseRepository, ISalespersonRepository
    {
        public SalespersonRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<IEnumerable<ISalesperson>> GetByDistrictId(int districtId,
            CancellationToken cancellationToken)
        {
            var sql = "SELECT S.* FROM Salesperson S " +
                      "INNER JOIN District D ON S.Id = D.PrimarySalespersonId " +
                      "WHERE D.Id = @districtId " +
                      "UNION " +
                      "SELECT S.* " +
                      "FROM Salesperson S " +
                      "INNER JOIN SalespersonDistrict SD ON S.Id = SD.SalespersonId " +
                      "INNER JOIN District D ON SD.DistrictId = D.Id " +
                      "WHERE D.Id = @districtId";

            var cmd = new CommandDefinition(sql, new { districtId }, cancellationToken: cancellationToken);
            return await _connectionFactory.CreateConnection().QueryAsync<Salesperson>(cmd);
        }
    }
}
