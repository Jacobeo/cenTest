using Core.Models;

namespace DataAccess.Repositories.Interfaces
{
    public interface IDistrictRepository
    {
        Task<IEnumerable<IDistrict>> GetAllAsync(CancellationToken cancellationToken);
        Task<IDistrictDetails> GetDistrictDetails(int id, CancellationToken cancellation);
    }
}
