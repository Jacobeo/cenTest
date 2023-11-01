using API.Controllers;
using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Backend.Core;
using API.Tests.FixtureCustomizations;

namespace API.Tests.Controllers
{
    public class ControllerTestBase
    {
        protected readonly Mock<IDataService> _dataServiceMock;
        protected readonly Mock<ILogger<DistrictController>> _loggerMock;
        protected readonly IFixture _fixture;

        public ControllerTestBase()
        {
            _dataServiceMock = new Mock<IDataService>();
            _loggerMock = new Mock<ILogger<DistrictController>>();
            _fixture = new Fixture().Customize(new FixtureCustomization());
        }
    }
}

