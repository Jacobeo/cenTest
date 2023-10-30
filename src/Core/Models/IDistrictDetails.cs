namespace Core.Models
{
    public interface IDistrictDetails
    {
        IEnumerable<IStore> Stores { get; }
        IEnumerable<ISalesperson> Salespersons { get; }
    }
}
