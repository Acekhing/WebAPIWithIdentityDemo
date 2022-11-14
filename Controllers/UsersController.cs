using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebAPIWithIdentityDemo.DTOs;
using WebAPIWithIdentityDemo.Models;

namespace WebAPIWithIdentityDemo.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController: ControllerBase
    {

        private readonly UserManager<User> _userManager;

        public UsersController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        private UserDTO ApplyTransformation(User user)
        {
            try
            {
                UserDTO userDTO = new()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    UserName = user.UserName,
                };
                return userDTO;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new ArgumentException("Error applying transformation");
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {

            // 2a47cd12-bc17-40e6-a437-303336b03584

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _userManager.FindByIdAsync(id);
                if (result == null)
                {
                    return NoContent();
                }
                return Ok(ApplyTransformation(result));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Problem("Something went wrong", statusCode: 500);
            }
        }
    }
}
