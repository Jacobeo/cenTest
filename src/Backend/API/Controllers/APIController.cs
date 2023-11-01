using Backend.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace API.Controllers
{
    public abstract class APIControllerBase : ControllerBase
    {
        protected readonly IDataService _dataService;
        protected readonly ILogger<DistrictController> _logger;

        public APIControllerBase(IDataService dataService, ILogger<DistrictController> logger)
        {
            _dataService = dataService;
            _logger = logger;
        }

        protected (string ErrorCode, string ErrorMessage) HandleException(Exception ex)
        {
            var errorCode = Guid.NewGuid().ToString();
            _logger.LogError($"ErrorCode: {errorCode}\n" + ex, "An error occurred while processing your request.");
            return (errorCode, "An error occurred while processing your request.");
        }
    }
}
