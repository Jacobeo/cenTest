using Backend.Core.Models;

namespace Backend.DataAccess.Models
{
    public class Salesperson : IEntity, ISalesperson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IsPrimary { get; set; }
    }
}
