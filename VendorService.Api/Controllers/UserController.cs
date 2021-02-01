using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using VendorService.Application.Mappers;
using VendorService.Application.Services.Interfaces;

namespace VendorService.Api.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var response = await _userService.Authenticate(model);
            _logger.LogInformation($"Login => message : {response.Message[0].Description}");
            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserRegisterModel userRegisterModel)
        {
            if (userRegisterModel is null)
            {
                return BadRequest();
            }

            var response = await _userService.Create(userRegisterModel);
            _logger.LogInformation($"Create User => message : {response.Message[0].Description}");
            return Ok(response);
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put([FromBody] UserRegisterModel userRegisterModel)
        {
            if (userRegisterModel is null)
            {
                return BadRequest();
            }

            var response = await _userService.Update(userRegisterModel);
            _logger.LogInformation($"Update User => message : {response.Message[0].Description}");
            return Ok(response);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _userService.Delete(id);
            _logger.LogInformation($"Delete User => message : {response.Message[0].Description}");
            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var response = await _userService.GetById(id);
            _logger.LogInformation($"Get User by id => message : {response.Message[0].Description}");
            return Ok(response);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> List()
        {
            var response = await _userService.List();
            _logger.LogInformation($"List Users => message : {response.Message[0].Description}");
            return Ok(response);
        }
    }
}