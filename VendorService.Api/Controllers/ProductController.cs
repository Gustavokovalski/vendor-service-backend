using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

        // GET: api/<ValuesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        //[HttpPost]
        ////[Authorize]
        //public async Task<IActionResult> Post([FromBody] ProductModel productModel)
        //{
        //    if (productModel is null)
        //    {
        //        return BadRequest();
        //    }

        //    var response = await _service.Create(productModel);
        //    return Ok(response);
        //}


        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}