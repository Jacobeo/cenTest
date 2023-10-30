using Backend.Core.Models;

namespace Backend.DataAccess.Repositories.Interfaces
{
    public interface IStoreRepository
    {
        public Task<IEnumerable<IStore>> GetByDistrictId(int districtId, CancellationToken cancellationToken);
    }
}
