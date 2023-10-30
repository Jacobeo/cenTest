using DataAccess;
using DataAccess.Repositories;
using DataAccess.Repositories.Interfaces;
using SimpleInjector;

namespace CompositionRoot.Extensions
{
    public static class ContainerExtensions
    {
        public static Container SetupRepositories(this Container container, string connectionString)
        {
            container.RegisterSingleton<IDbConnectionFactory>(() => new DbConnectionFactory(connectionString));

            container.Register<ISalespersonRepository, SalespersonRepository>(Lifestyle.Scoped);
            container.Register<IDistrictRepository, DistrictRepository>(Lifestyle.Scoped);
            container.Register<IStoreRepository, StoreRepository>(Lifestyle.Scoped);

            return container;
        }
    }
}
