namespace Core.Models
{
    public class Salesperson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Display => $"{Name} - {IsPrimary}";
        public string IsPrimary { get; set; }
        public bool IsPrimaryBool => IsPrimary == "Primary";
    }
}
