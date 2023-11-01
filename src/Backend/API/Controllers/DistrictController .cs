using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Backend.Core.Requests;
using Backend.Core;
using Backend.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DistrictController : APIControllerBase
    {
        public DistrictController(IDataService dataService, ILogger<DistrictController> logger): base(dataService, logger)
        {
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IDistrict>>> GetDistricts(CancellationToken cancellationToken)
        {
            try
            {
                var districts = await _dataService.GetAllDistricts(cancellationToken);

                if (districts.ToList().Count == 0)
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
                var districtDetails = await _dataService.GetDistrictDetails(districtId, cancellationToken);

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

        [HttpPost("AddSalesPerson")]
        public async Task<IActionResult> AddSalesPerson([FromBody] AddSalesPersonRequest request, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _dataService.AddSalesPersonToDistrict(request.DistrictId,request.SalesPersonId, request.IsPrimary, cancellationToken);
                    return Ok("SalesPerson added successfully.");
                }
                catch (Exception ex)
                {
                    var (errorCode, errorMessage) = HandleException(ex);
                    return StatusCode(500, new { errorCode, errorMessage });
                }
            }
            return BadRequest("Invalid request");
        }
        [HttpDelete("DeleteSalesPerson")]

        public async Task<IActionResult> DeleteSalesPerson([FromBody] DeleteSalesPersonRequest request, CancellationToken cancellationToken)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _dataService.DeleteSalesPersonFromDistrict(request.DistrictId, request.SalesPersonId, cancellationToken);
                    return Ok("SalesPerson deleted successfully.");
                }
                catch (Exception ex)
                {
                    var (errorCode, errorMessage) = HandleException(ex);
                    return StatusCode(500, new { errorCode, errorMessage });
                }
            }
            return BadRequest("Invalid request");
        }
    }
}
