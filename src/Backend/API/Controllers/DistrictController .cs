using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Core.Models;
using DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictController : ControllerBase
    {
        private readonly IDistrictRepository _districtRepository;
        private readonly ILogger<DistrictController> _logger;

        public DistrictController(IDistrictRepository districtRepository, ILogger<DistrictController> logger)
        {
            _districtRepository = districtRepository;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IDistrict>>> GetDistricts(CancellationToken cancellationToken)
        {
            try
            {
                var districts = await _districtRepository.GetAllAsync(cancellationToken);

                if (districts == null)
                {
                    return NotFound();
                }

                return Ok(districts);
            }
            catch (Exception ex)
            {
                var (errorCode, errorMessage) = HandleException(ex);
                return StatusCode(500, new { errorCode, errorMessage });
            }
        }

        [HttpGet("{districtId}")]
        public async Task<ActionResult<IDistrictDetails>> GetDistrictDetails(int districtId,
            CancellationToken cancellationToken)
        {
            try
            {
                var districtDetails = await _districtRepository.GetDistrictDetails(districtId, cancellationToken);

                if (districtDetails == null)
                {
                    return NotFound();
                }

                return Ok(districtDetails);
            }
            catch (Exception ex)
            {
                var (errorCode, errorMessage) = HandleException(ex);
                return StatusCode(500, new { errorCode, errorMessage });
            }
        }

        private (string ErrorCode, string ErrorMessage) HandleException(Exception ex)
        {
            var errorCode = Guid.NewGuid().ToString();
            _logger.LogError($"ErrorCode: {errorCode}\n" + ex, "An error occurred while processing your request.");
            return (errorCode, "An error occurred while processing your request.");
        }
    }
}
