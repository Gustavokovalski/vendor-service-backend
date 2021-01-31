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
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
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
    }
}