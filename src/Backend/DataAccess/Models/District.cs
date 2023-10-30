using Core.Models;

namespace DataAccess.Models
{
    public class District : IEntity, IDistrict
    {
        public Salesperson PrimarySalesperson { get; set; }
        public string Name { get; set; }
        public int PrimarySalespersonId { get; set; }
        public int Id { get; set; }
    }
}
