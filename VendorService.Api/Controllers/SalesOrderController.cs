using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using VendorService.Application.Mappers;
using VendorService.Application.Services.Interfaces;

namespace VendorService.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class SalesOrderController : ControllerBase
    {
        private readonly ISalesOrderService _service;
        private readonly ILogger<SalesOrderController> _logger;
        public SalesOrderController(ISalesOrderService service, ILogger<SalesOrderController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Post([FromBody] SalesOrderModel salesOrderModel)
        {
            if (salesOrderModel is null)
            {
                return BadRequest();
            }

            var response = await _service.Create(salesOrderModel);
            _logger.LogInformation($"Create Sales Order => message : {response.Message[0].Description}");
            return Ok(response);
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Put([FromBody] SalesOrderModel salesOrderModel)
        {
            if (salesOrderModel is null)
            {
                return BadRequest();
            }

            var response = await _service.Update(salesOrderModel);
            _logger.LogInformation($"Update Sales Order => message : {response.Message[0].Description}");
            return Ok(response);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _service.Delete(id);
            _logger.LogInformation($"Delete Sales Order => message : {response.Message[0].Description}");
            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _service.GetById(id);
            _logger.LogInformation($"Get Sales Order by id => message : {response.Message[0].Description}");
            return Ok(response);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Employee,Customer")]
        public async Task<IActionResult> List()
        {
            var response = await _service.List();
            _logger.LogInformation($"List Sales Order => message : {response.Message[0].Description}");
            return Ok(response);
        }

        [HttpGet("productorder/{id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> GetByOrderId(int id)
        {
            var response = await _service.GetByOrderId(id);
            _logger.LogInformation($"Get Product Order => message : {response.Message[0].Description}");
            return Ok(response);
        }
    }
}
