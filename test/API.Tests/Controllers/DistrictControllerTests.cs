using API.Controllers;
using AutoFixture;
using Backend.Core.Models;
using Backend.Core.Requests;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Tests.Controllers
{
    public class DistrictControllerTests : ControllerTestBase
    {
        protected readonly DistrictController _controller;
        public DistrictControllerTests() 
        {
            _controller = new DistrictController(_dataServiceMock.Object, _loggerMock.Object);

        }

        [Fact]
        public async Task GetDistricts_ShouldReturnNotFound_WhenNoDistrictsExist()
        {
            // Arrange
            _dataServiceMock.Setup(x => x.GetAllDistricts(It.IsAny<CancellationToken>()))
                .ReturnsAsync(Enumerable.Empty<IDistrict>());

            // Act
            var result = await _controller.GetDistricts(CancellationToken.None);

            // Assert
            result.Result.ShouldBeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetDistricts_ShouldReturnOk_WithDistricts()
        {
            // Arrange
            var districts = _fixture.CreateMany<IDistrict>(5).ToList();
            _dataServiceMock.Setup(x => x.GetAllDistricts(It.IsAny<CancellationToken>()))
                .ReturnsAsync(districts);

            // Act
            var result = await _controller.GetDistricts(CancellationToken.None);

            // Assert
            var okResult = result.Result.ShouldBeOfType<OkObjectResult>();
            var returnedDistricts = okResult.Value.ShouldBeAssignableTo<IEnumerable<IDistrict>>();
            returnedDistricts.Count().ShouldBe(5);
        }

        [Fact]
        public async Task GetDistrictDetails_ShouldReturnNotFound_WhenDistrictDoesNotExist()
        {
            // Arrange
            _dataServiceMock.Setup(x => x.GetDistrictDetails(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((IDistrictDetails)null);

            // Act
            var result = await _controller.GetDistrictDetails(1, CancellationToken.None);

            // Assert
            result.Result.ShouldBeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetDistrictDetails_ShouldReturnOk_WithDistrictDetails()
        {
            // Arrange
            var districtDetails = _fixture.Create<IDistrictDetails>();
            _dataServiceMock.Setup(x => x.GetDistrictDetails(It.IsAny<int>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(districtDetails);

            // Act
            var result = await _controller.GetDistrictDetails(1, CancellationToken.None);

            // Assert
            var okResult = result.Result.ShouldBeOfType<OkObjectResult>();
            var returnedDistrictDetails = okResult.Value.ShouldBeAssignableTo<IDistrictDetails>();
            returnedDistrictDetails.ShouldNotBeNull();
        }

        [Fact]
        public async Task AddSalesPerson_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Test", "TestError");

            var request = _fixture.Create<AddSalesPersonRequest>();

            // Act
            var result = await _controller.AddSalesPerson(request, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task AddSalesPerson_ShouldReturnOk_WhenSalesPersonIsAdded()
        {
            // Arrange
            var request = _fixture.Create<AddSalesPersonRequest>();

            // Act
            var result = await _controller.AddSalesPerson(request, CancellationToken.None);

            // Assert
            var okResult = result.ShouldBeOfType<OkObjectResult>();
            okResult.Value.ShouldBe("SalesPerson added successfully.");
        }

        [Fact]
        public async Task DeleteSalesPerson_ShouldReturnBadRequest_WhenModelStateIsInvalid()
        {
            // Arrange
            _controller.ModelState.AddModelError("Test", "TestError");

            var request = _fixture.Create<DeleteSalesPersonRequest>();

            // Act
            var result = await _controller.DeleteSalesPerson(request, CancellationToken.None);

            // Assert
            result.ShouldBeOfType<BadRequestObjectResult>();
        }

        [Fact]
        public async Task DeleteSalesPerson_ShouldReturnOk_WhenSalesPersonIsDeleted()
        {
            // Arrange
            var request = _fixture.Create<DeleteSalesPersonRequest>();

            // Act
            var result = await _controller.DeleteSalesPerson(request, CancellationToken.None);

            // Assert
            var okResult = result.ShouldBeOfType<OkObjectResult>();
            okResult.Value.ShouldBe("SalesPerson deleted successfully.");
        }


    }
}
