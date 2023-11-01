using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.DataAccess.Models;
using Backend.DataAccess.Repositories;
using Backend.DataAccess.Repositories.Interfaces;
using Moq;

namespace DataAccess.Tests
{
    public class SalespersonRepositoryTests
    {
        [Fact]
        public async Task GetByDistrictId_ReturnsCorrectSalespersons()
        {
            // Arrange
            var districtId = 1;
            var cancellationToken = CancellationToken.None;

            var mockConnectionFactory = new Mock<IDbConnectionFactory>();
            var mockDatabaseProvider = new Mock<IDatabaseProvider>();
            var repository = new SalespersonRepository(mockConnectionFactory.Object, mockDatabaseProvider.Object);

            var expectedSalespersons = new List<Salesperson>
            {
                new Salesperson { Id = 1, Name = "Salesperson 1", IsPrimary = "Primary" },
                new Salesperson { Id = 2, Name = "Salesperson 2", IsPrimary = "Secondary" }
            };

            mockDatabaseProvider.Setup(m => m.QueryAsync<Salesperson>(
                    It.IsAny<IDbConnection>(),
                    It.IsAny<string>(),
                    It.IsAny<object>(),
                    null, null, null))
                .ReturnsAsync(expectedSalespersons);

            // Act
            var result = await repository.GetByDistrictId(districtId, cancellationToken);

            // Assert
            var salespersonsList = result.ToList();
            Assert.Equal(expectedSalespersons.Count, salespersonsList.Count);
            for (int i = 0; i < expectedSalespersons.Count; i++)
            {
                Assert.Equal(expectedSalespersons[i].Id, salespersonsList[i].Id);
                Assert.Equal(expectedSalespersons[i].Name, salespersonsList[i].Name);
                Assert.Equal(expectedSalespersons[i].IsPrimary, salespersonsList[i].IsPrimary);
            }
        }
    }

}
