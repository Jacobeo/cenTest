using Backend.DataAccess.Models;
using Backend.DataAccess.Repositories;
using Backend.DataAccess.Repositories.Interfaces;
using Moq;
using System.Data;

namespace DataAccess.Tests
{
    public class StoreRepositoryTests
    {
        [Fact]
        public async Task GetByDistrictId_ReturnsCorrectStores()
        {
            // Arrange
            var districtId = 1;
            var cancellationToken = CancellationToken.None;

            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockDapperProvider = new Mock<IDatabaseProvider>();
            var repository = new StoreRepository(mockConnectionFactory.Object, mockDapperProvider.Object);

            var expectedStores = new List<Store>
            {
                new Store { Id = 1, Name = "Store 1", DistrictId = 1 },
                new Store { Id = 2, Name = "Store 2", DistrictId = 1 }
            };

            mockDapperProvider.Setup(m => m.QueryAsync<Store>(
                    It.IsAny<IDbConnection>(),
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    null, null, null))
                .ReturnsAsync(expectedStores);

            // Act
            var result = await repository.GetByDistrictId(districtId, cancellationToken);

            // Assert
            var storesList = result.ToList();
            Assert.Equal(expectedStores.Count, storesList.Count);
            for (int i = 0; i < expectedStores.Count; i++)
            {
                Assert.Equal(expectedStores[i].Id, storesList[i].Id);
                Assert.Equal(expectedStores[i].Name, storesList[i].Name);
                Assert.Equal(expectedStores[i].DistrictId, storesList[i].DistrictId);
            }
        }

    }
}
