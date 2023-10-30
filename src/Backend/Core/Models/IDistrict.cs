namespace Backend.Core.Models
{
    public interface IDistrict
    {
        int Id { get; set; }
        string Name { get; set; }
        int PrimarySalespersonId { get; set; }
    }
}
