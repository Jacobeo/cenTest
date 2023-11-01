using Backend.Core.Models;
using Backend.Core.Requests;

namespace Backend.Core
{
    public interface IDataService
    {
        Task<IEnumerable<IDistrict>> GetAllDistricts(CancellationToken cancellationToken);
        Task<IEnumerable<ISalesperson>> GetAllSalespersons(CancellationToken cancellationToken);
        Task<IDistrictDetails> GetDistrictDetails(int id, CancellationToken cancellationToken);

        Task AddSalesPersonToDistrict(int districtId, int salesPersonId, bool IsPrimary, CancellationToken cancellation);
        Task DeleteSalesPersonFromDistrict(int districtId, int salesPersonId, CancellationToken cancellation);
    }
}
