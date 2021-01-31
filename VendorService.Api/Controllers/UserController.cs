using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var result = await _userService.Authenticate(model);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserRegisterModel userRegisterModel)
        {
            if (userRegisterModel is null)
            {
                return BadRequest();
            }

            var response = await _userService.Create(userRegisterModel);
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
            return Ok(response);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await _userService.Delete(id);
            return Ok(response);
        }

        [HttpGet]
        [Route("profiles")]
        public IActionResult GetProfiles()
        {
            var response = _userService.ListProfiles();
            return Ok(response);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        //[AllowAnonymous]
        public async Task<IActionResult> List()
        {
            var response = await _userService.List();
            return Ok(response);
        }
    }
}