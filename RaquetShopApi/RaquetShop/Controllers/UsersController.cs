using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using RaquetShop.Models;
using RaquetShop.Models.Security;
using RaquetShop.Services.Security;
namespace RaquetShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        // /api/users/userx  
        [HttpPost("User")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.RegisterUserAsync(model);

                if (result.IsSuccess)
                    return Ok(result); // Status Code: 200 

                return BadRequest(result);
            }

            return BadRequest("Some properties are not valid"); // Status code: 400
        }

        [HttpPost("Role")]
        public async Task<IActionResult> CreateRolenAsync([FromBody] CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.CreateRoleAsync(model);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            return BadRequest("Some properties are not valid");
        }

        [HttpPost("UserRole")]
        public async Task<IActionResult> CreateUserRolenAsync([FromBody] CreateUserRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.CreateUserRoleAsync(model);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            return BadRequest("Algunas propiedades no son validas.");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

                  var result = await userService.LoginUserAsync(model);

                  if (result.IsSuccess)
                  {
                      return Ok(result);
                  }

                  return BadRequest(result);
            }

            return BadRequest("Algunas propiedades no son validas.");
        }
        [HttpPost("UserRoleSimple")]
        public async Task<IActionResult> CreateUserRolenSimpleAsync([FromBody] User model)
        {
            if (ModelState.IsValid)
            {
                var result = await userService.CreateUserRolenSimpleAsync(model);

                if (result.IsSuccess)
                {
                    return Ok(result);
                }

                return BadRequest(result);
            }
            return BadRequest("Algunas propiedades no son validas.");
        }
    }
}
