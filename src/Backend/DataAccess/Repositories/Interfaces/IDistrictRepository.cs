using Backend.Core.Models;

namespace Backend.DataAccess.Repositories.Interfaces
{
    public interface IDistrictRepository
    {
        Task<IEnumerable<IDistrict>> GetAllAsync(CancellationToken cancellationToken);
    }
}
