using Core;
using Core.Models;
using DataAccess.Models;
using Newtonsoft.Json;

namespace UI
{
    public class APIDataService : IDataService
    {
        private readonly HttpClient _httpClient;

        public APIDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            // _httpClient.BaseAddress = new Uri("https://localhost:5001");
        }

        public async Task<List<District>> GetDistrictsAsync()
        {
            // Call the endpoint to get the list of districts
            // Deserialize the response and return the list of districts
            var response = await _httpClient.GetAsync("api/district");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var districts = JsonConvert.DeserializeObject<List<DistrictDto>>(content);
            return districts.Select(d => new District { Id = d.Id, Name = d.Name }).ToList();
        }

        public async Task<DistrictDetail> GetDistrictDetailsAsync(int districtId)
        {
            // Call the endpoint to get the district details
            // Deserialize the response and return the district details
            var response = await _httpClient.GetAsync($"api/district/{districtId}");
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var details = JsonConvert.DeserializeObject<DistrictDetailDto>(content);
            var hest = new DistrictDetail
            {
                Stores = details.Stores.Select(s => new Store { Id = s.Id, Name = s.Name }).ToList(),
                Salespersons = details.Salespersons.Select(s => new Salesperson { Id = s.Id, Name = s.Name }).ToList(),
            };

            return new DistrictDetail
            {
                Stores = details.Stores.Select(s => new Store { Id = s.Id, Name = s.Name }).ToList(),
                Salespersons = details.Salespersons.Select(s => new Salesperson { Id = s.Id, Name = s.Name }).ToList(),
            };
        }
    }
}
