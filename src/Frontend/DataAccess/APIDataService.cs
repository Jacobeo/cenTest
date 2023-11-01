using System.Text;
using Core;
using Core.Models;
using DataAccess.Models;
using Newtonsoft.Json;
using Polly;
using Polly.Retry;

namespace UI
{
    public class APIDataService : IDataService
    {
        private readonly HttpClient _httpClient;
        private readonly AsyncRetryPolicy<HttpResponseMessage> _retryPolicy;


        public APIDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _retryPolicy = Policy
                .Handle<HttpRequestException>()
                .OrResult<HttpResponseMessage>(r => (int)r.StatusCode >= 500)
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
        }

        public async Task<List<District>> GetDistrictsAsync()
        {
            var response = await _retryPolicy.ExecuteAsync(() =>_httpClient.GetAsync("api/district"));
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var districts = JsonConvert.DeserializeObject<List<DistrictDto>>(content);
            return districts.Select(d => new District { Id = d.Id, Name = d.Name }).ToList();
        }

        public async Task<DistrictDetail> GetDistrictDetailsAsync(int districtId)
        {
            var response = await _retryPolicy.ExecuteAsync(() =>_httpClient.GetAsync($"api/district/{districtId}"));
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var details = JsonConvert.DeserializeObject<DistrictDetailDto>(content);

            return new DistrictDetail
            {
                Stores = details.Stores.Select(s => new Store { Id = s.Id, Name = s.Name }).ToList(),
                Salespersons = details.Salespersons.Select(s => new Salesperson { Id = s.Id, Name = s.Name, IsPrimary = s.IsPrimary}).ToList(),
            };
        }

        public async Task<List<Salesperson>> GetAllSalesPersons()
        {
            var response = await _retryPolicy.ExecuteAsync(() =>_httpClient.GetAsync($"api/salesperson"));
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var salespersons = JsonConvert.DeserializeObject<List<SalespersonDto>>(content);
            return salespersons.Select(s => new Salesperson
            {
                Id = s.Id,
                Name = s.Name,
                IsPrimary = s.IsPrimary

            }).ToList();
        }

        public async Task AddSalesPersonToDistrict(int districtId, int salesPersonId, bool IsPrimary)
        {
            var request = new AddSalesPersonToDistrictRequest()
            {
                DistrictId = districtId,
                SalesPersonId = salesPersonId,
                IsPrimary = IsPrimary
            };

            var json = JsonConvert.SerializeObject(request);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _retryPolicy.ExecuteAsync(() => _httpClient.PostAsync("api/District/AddSalesPerson", content));
            response.EnsureSuccessStatusCode();
        }
    }
}
