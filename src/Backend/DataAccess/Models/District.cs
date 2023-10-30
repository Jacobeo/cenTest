using Backend.Core.Models;

namespace Backend.DataAccess.Models
{
    public class District : IEntity, IDistrict
    {
        public string Name { get; set; }
        public int PrimarySalespersonId { get; set; }
        public int Id { get; set; }
    }
}
