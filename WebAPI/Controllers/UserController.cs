using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUserService userService;

        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        [Route("/users")]
        public async Task<ActionResult<IList<User>>> GetUsersAsync()
            // [FromQuery] int? id, [FromQuery] string userName, [FromQuery] string password,
            // [FromQuery] string role, [FromQuery] int? securityLevel
        {
            try
            {
                IList<User> users = await userService.GetUsersAsync();
                foreach (var u in users)
                {
                    Console.WriteLine(u.UserName);
                }
                return Ok(users);
   
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<User>> GetUserAsync([FromRoute] int? id)
        {
            try
            {
                User user = await userService.GetUserAsync(id);
                return Ok(user);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult> RemoveUserAsync([FromQuery] int? id)
        {
            try
            {
                await userService.RemoveUserAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<User>> AddUserAsync([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await userService.AddUserAsync(user);
               // User added = await userService.AddUserAsync(user);
               // return Created($"/User/{user.UserName}", added);
               return StatusCode(200);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpPatch]
        [Route("{id:int}")]
        public async Task<ActionResult<User>> UpdateUserAsync([FromBody] User user)
        {
            try
            {
                User updatedUser = await userService.UpdateUserAsync(user);
                return Ok(updatedUser);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(500, e.Message);
            }
        }
        
        [HttpGet]
        public async Task<ActionResult<User>> ValidateUserAsync([FromQuery]string userName, [FromQuery]string password)
        {
            try
            {
                var user = await userService.ValidateUserAsync(userName, password);
                return Ok(user);
            }
            catch (Exception e)
            {
                return BadRequest("crap");
            }
        }
        
    }
}