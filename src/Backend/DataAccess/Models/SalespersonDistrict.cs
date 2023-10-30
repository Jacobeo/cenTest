using Backend.Core.Models;

namespace Backend.DataAccess.Models
{
    public class SalespersonDistrict : IEntity, ISalespersonDistrict
    {
        public int SalespersonId { get; set; }
        public Salesperson Salesperson { get; set; }
        public District District { get; set; }
        public int Id { get; set; }

        public int DistrictId { get; set; }
    }
}
