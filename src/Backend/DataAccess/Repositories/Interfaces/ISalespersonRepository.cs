using Core.Models;

namespace DataAccess.Repositories.Interfaces
{
    public interface ISalespersonRepository
    {
        public Task<IEnumerable<ISalesperson>> GetByDistrictId(int districtId, CancellationToken cancellationToken);
    }
}
