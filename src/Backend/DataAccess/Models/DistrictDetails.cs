using Backend.Core.Models;

namespace Backend.DataAccess.Models
{
    public record DistrictDetails
        (IEnumerable<IStore> Stores, IEnumerable<ISalesperson> Salespersons) : IDistrictDetails;
}
