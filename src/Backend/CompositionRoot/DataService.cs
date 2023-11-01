using Backend.Core;
using Backend.Core.Models;
using Backend.Core.Requests;
using Backend.DataAccess.Models;
using Backend.DataAccess.Repositories.Interfaces;

namespace Backend.CompositionRoot
{
    public class DataService : IDataService
    {
        private readonly IDistrictRepository _districtRepository;
        private readonly ISalespersonRepository _salespersonRepository;
        private readonly IStoreRepository _storeRepository;

        public DataService(ISalespersonRepository salespersonRepository, IStoreRepository storeRepository,
            IDistrictRepository districtRepository)
        {
            _salespersonRepository = salespersonRepository;
            _storeRepository = storeRepository;
            _districtRepository = districtRepository;
        }

        public async Task AddSalesPersonToDistrict(int districtId, int salesPersonId, bool isPrimary, CancellationToken cancellationToken)
        {
            if (isPrimary)
            {
                await _districtRepository.AddPrimarySalesperson(districtId, salesPersonId, cancellationToken);
                await _districtRepository.DeleteSalesperson(districtId, salesPersonId, cancellationToken);
            }
            else
            {
                await _districtRepository.AddSecondarySalesperson(districtId, salesPersonId, cancellationToken);
            }
        }

        public async Task DeleteSalesPersonFromDistrict(int districtId, int salesPersonId, CancellationToken cancellation)
        {
            await _districtRepository.DeleteSalesperson(districtId, salesPersonId, cancellation);
        }

        public Task<IEnumerable<IDistrict>> GetAllDistricts(CancellationToken cancellationToken)
        {
            return _districtRepository.GetAllAsync(cancellationToken);
        }

        public Task<IEnumerable<ISalesperson>> GetAllSalespersons(CancellationToken cancellationToken)
        {
            return _salespersonRepository.GetAllAsync(cancellationToken);
        }

        public async Task<IDistrictDetails> GetDistrictDetails(int id, CancellationToken cancellationToken)
        {
            var salespersons = await _salespersonRepository.GetByDistrictId(id, cancellationToken);
            var stores = await _storeRepository.GetByDistrictId(id, cancellationToken);

            return new DistrictDetails(stores, salespersons);
        }
    }
}
