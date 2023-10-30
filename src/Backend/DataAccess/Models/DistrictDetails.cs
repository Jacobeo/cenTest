using Core.Models;

namespace DataAccess.Models
{
    public record DistrictDetails
        (IEnumerable<IStore> Stores, IEnumerable<ISalesperson> Salespersons) : IDistrictDetails;
}
