namespace Backend.Core.Models
{
    public interface ISalesperson
    {
        int Id { get; set; }
        string Name { get; set; }
        string IsPrimary { get; set; }
    }
}
