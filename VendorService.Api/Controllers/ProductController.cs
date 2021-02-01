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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductService service, ILogger<ProductController> logger)
        {
            _service = service;
            _logger = logger;
        }

  
        [HttpPost]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Post([FromBody] ProductModel productModel)
        {
            if (productModel is null)
            {
                return BadRequest();
            }

            var response = await _service.Create(productModel);
            _logger.LogInformation($"Create Product => message : {response.Message[0].Description}");
            return Ok(response);
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Put([FromBody] ProductModel productModel)
        {
            if (productModel is null)
            {
                return BadRequest();
            }

            var response = await _service.Update(productModel);
            _logger.LogInformation($"Update Product => message : {response.Message[0].Description}");
            return Ok(response);
        }

        [HttpPut]
        [Route("inactivate")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> Inactivate([FromBody] ProductModel productModel)
        {
            if (productModel is null)
            {
                return BadRequest();
            }
            var response = await _service.Inactivate(productModel);
            _logger.LogInformation($"Inactivate Product => message : {response.Message[0].Description}");
            return Ok(response);
        }


        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> GetById(int id)
        {
            var response = await _service.GetById(id);
            _logger.LogInformation($"Get Product by id => message : {response.Message[0].Description}");
            return Ok(response);
        }

        [HttpGet]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<IActionResult> List()
        {
            var response = await _service.List();
            _logger.LogInformation($"List Products => message : {response.Message[0].Description}");
            return Ok(response);
        }
    }
}