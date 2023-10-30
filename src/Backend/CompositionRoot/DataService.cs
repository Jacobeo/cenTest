using Backend.Core;
using Backend.Core.Models;
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

        public Task<IEnumerable<IDistrict>> GetAllDistricts(CancellationToken cancellationToken)
        {
            return _districtRepository.GetAllAsync(cancellationToken);
        }

        public async Task<IDistrictDetails> GetDistrictDetails(int id, CancellationToken cancellationToken)
        {
            var salespersons = await _salespersonRepository.GetByDistrictId(id, cancellationToken);
            var stores = await _storeRepository.GetByDistrictId(id, cancellationToken);

            return new DistrictDetails(stores, salespersons);
        }
    }
}
