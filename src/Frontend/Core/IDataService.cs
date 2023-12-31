﻿using Core.Models;

namespace Core
{
    public interface IDataService
    {
        Task<List<District>> GetDistrictsAsync();
        Task<DistrictDetail> GetDistrictDetailsAsync(int districtId);
        Task<List<Salesperson>> GetAllSalesPersons();
        Task AddSalesPersonToDistrict(int districtId, int salesPersonId, bool IsPrimary);
    }
}
