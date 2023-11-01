using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Backend.Core;
using Backend.Core.Models;
using Backend.DataAccess.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalespersonController : APIControllerBase
    {
        public SalespersonController(IDataService dataService, ILogger<DistrictController> logger) : base(dataService, logger)
        {
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ISalesperson>>> GetSalespersons(CancellationToken cancellationToken)
        {
            try
            {
                var salespersons = await _dataService.GetAllSalespersons(cancellationToken);

                if (salespersons.ToList().Count == 0)
                {
                    return NotFound();
                }

                return Ok(salespersons);
            }
            catch (Exception ex)
            {
                var (errorCode, errorMessage) = HandleException(ex);
                return StatusCode(500, new { errorCode, errorMessage });
            }
        }
    }
}
