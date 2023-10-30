﻿using Core.Models;
using Dapper;
using DataAccess.Models;
using DataAccess.Repositories.Interfaces;

namespace DataAccess.Repositories
{
    public class StoreRepository : BaseRepository, IStoreRepository
    {
        public StoreRepository(IDbConnectionFactory connectionFactory) : base(connectionFactory)
        {
        }

        public async Task<IEnumerable<IStore>> GetByDistrictId(int districtId, CancellationToken cancellationToken)
        {
            var sql = "SELECT * FROM Store where DistrictId = @districtId";

            var cmd = new CommandDefinition(sql, new { districtId }, cancellationToken: cancellationToken);
            return await _connectionFactory.CreateConnection().QueryAsync<Store>(cmd);
        }
    }
}
