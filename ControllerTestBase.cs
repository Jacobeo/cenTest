using System;

namespace
{
public class ControllerTestBase
{

    private readonly DistrictController _controller;
    private readonly Mock<IDataService> _dataServiceMock;
    private readonly Mock<ILogger<DistrictController>> _loggerMock;
    private readonly IFixture _fixture;

    public DistrictControllerTests()
    {
        _dataServiceMock = new Mock<IDataService>();
        _loggerMock = new Mock<ILogger<DistrictController>>();
        _controller = new DistrictController(_dataServiceMock.Object, _loggerMock.Object);
        _fixture = new Fixture();
    }
}

}



