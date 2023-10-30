using Backend.Core.Models;

namespace Backend.Core
{
    public interface IDataService
    {
        Task<IEnumerable<IDistrict>> GetAllDistricts(CancellationToken cancellationToken);
        Task<IDistrictDetails> GetDistrictDetails(int id, CancellationToken cancellationToken);
    }
}
