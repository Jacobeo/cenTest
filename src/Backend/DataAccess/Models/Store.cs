using Backend.Core.Models;

namespace Backend.DataAccess.Models
{
    public class Store : IEntity, IStore
    {
        public District District { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public int DistrictId { get; set; }
    }
}
