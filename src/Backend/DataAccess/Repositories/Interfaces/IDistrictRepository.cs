using Backend.Core.Models;

namespace Backend.DataAccess.Repositories.Interfaces
{
    public interface IDistrictRepository
    {
        Task<IEnumerable<IDistrict>> GetAllAsync(CancellationToken cancellationToken);

        Task AddPrimarySalesperson(int districtId, int salespersonId, CancellationToken cancellationToken);
        Task AddSecondarySalesperson(int districtId, int salespersonId, CancellationToken cancellationToken);
        Task DeleteSalesperson(int districtId, int salesPersonId, CancellationToken cancellationToken);
    }
}
