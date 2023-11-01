using Backend.Core.Models;

namespace Backend.DataAccess.Repositories.Interfaces
{
    public interface ISalespersonRepository
    {
        public Task<IEnumerable<ISalesperson>> GetByDistrictId(int districtId, CancellationToken cancellationToken);
        public Task<IEnumerable<ISalesperson>> GetAllAsync(CancellationToken cancellationToken);
    }
}
