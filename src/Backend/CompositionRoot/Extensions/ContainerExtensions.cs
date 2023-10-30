using Backend.Core;
using Backend.DataAccess;
using Backend.DataAccess.Repositories;
using Backend.DataAccess.Repositories.Interfaces;
using SimpleInjector;

namespace Backend.CompositionRoot.Extensions
{
    public static class ContainerExtensions
    {
        public static Container SetupRepositories(this Container container, string connectionString)
        {
            container.RegisterSingleton<IDbConnectionFactory>(() => new DbConnectionFactory(connectionString));

            container.Register<ISalespersonRepository, SalespersonRepository>(Lifestyle.Scoped);
            container.Register<IDistrictRepository, DistrictRepository>(Lifestyle.Scoped);
            container.Register<IStoreRepository, StoreRepository>(Lifestyle.Scoped);
            container.Register<IDataService, DataService>(Lifestyle.Scoped);

            return container;
        }
    }
}
