using Core.Models;

namespace DataAccess.Models
{
    public class Salesperson : IEntity, ISalesperson
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
