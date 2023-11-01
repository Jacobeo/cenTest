using Backend.DataAccess.Models;
using Backend.DataAccess.Repositories;
using Backend.DataAccess.Repositories.Interfaces;
using Moq;
using System.Data;

namespace DataAccess.Tests
{
    public class DistrictRepositoryTests
    {
        [Fact]
        public async Task AddPrimarySalesperson_ExecutesExpectedQuery()
        {
            // Arrange
            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockDatabaseProvider = new Mock<IDatabaseProvider>();
            var mockDbConnection = new Mock<IDbConnection>();

            mockConnectionFactory.Setup(m => m.CreateConnection()).Returns(mockDbConnection.Object);
            var mockDapperProvider = new Mock<IDatabaseProvider>();

            var repository = new DistrictRepository(mockConnectionFactory.Object, mockDatabaseProvider.Object);

            var districtId = 1;
            var salespersonId = 2;
            var cancellationToken = new CancellationToken();

            // Act
            await repository.AddPrimarySalesperson(districtId, salespersonId, cancellationToken);

            // Assert
            mockDatabaseProvider.Verify(m => m.ExecuteAsync(
                mockDbConnection.Object,
                "UPDATE District SET PrimarySalespersonId = @salespersonId WHERE Id = @districtId",
                It.Is<object>(o =>
                    (int)o.GetType().GetProperty("districtId").GetValue(o) == districtId &&
                    (int)o.GetType().GetProperty("salespersonId").GetValue(o) == salespersonId
                ),
                null,
                null,
                null,
                cancellationToken
            ), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllSalespersons()
        {
            // Arrange
            var cancellationToken = CancellationToken.None;

            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockDatabaseProvider = new Mock<IDatabaseProvider>();
            var repository = new SalespersonRepository(mockConnectionFactory.Object, mockDatabaseProvider.Object);

            var expectedSalespersons = new List<Salesperson>
            {
                new Salesperson { Id = 1, Name = "Salesperson 1" },
                new Salesperson { Id = 2, Name = "Salesperson 2" }
            };

            mockDatabaseProvider.Setup(m => m.QueryAsync<Salesperson>(
                    It.IsAny<IDbConnection>(),
                    It.IsAny<string>(),
                    null, null, null, null))
                .ReturnsAsync(expectedSalespersons);

            // Act
            var result = await repository.GetAllAsync(cancellationToken);

            // Assert
            var salespersonsList = result.ToList();
            Assert.Equal(expectedSalespersons.Count, salespersonsList.Count);
            for (int i = 0; i < expectedSalespersons.Count; i++)
            {
                Assert.Equal(expectedSalespersons[i].Id, salespersonsList[i].Id);
                Assert.Equal(expectedSalespersons[i].Name, salespersonsList[i].Name);
            }
        }

    }

}
