namespace Backend.Core.Requests
{
    public class AddSalesPersonRequest
    {
        public int DistrictId { get; set; }
        public int SalesPersonId { get; set; }
        public bool IsPrimary { get; set; }
    }
}
