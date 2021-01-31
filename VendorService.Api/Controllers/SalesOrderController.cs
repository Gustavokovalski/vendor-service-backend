using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        public SalesOrderController(ISalesOrderService service)
        {
            _service = service;
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
            return Ok(response);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _service.Delete(id);
            return Ok(response);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _service.GetById(id);
            return Ok(response);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> List()
        {
            var response = await _service.List();
            return Ok(response);
        }

        [HttpGet("productorder/{id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> GetByOrderId(int id)
        {
            var response = await _service.GetByOrderId(id);
            return Ok(response);
        }
    }
}
