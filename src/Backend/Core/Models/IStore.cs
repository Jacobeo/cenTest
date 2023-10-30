namespace Backend.Core.Models
{
    public interface IStore
    {
        int Id { get; set; }
        string Name { get; set; }
        int DistrictId { get; set; }
    }
}
