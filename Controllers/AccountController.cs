using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using WebAPIWithIdentityDemo.DTOs;
using WebAPIWithIdentityDemo.Models;

namespace WebAPIWithIdentityDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        private User ApplyTransformation(RegisterDTO registerDTO)
        {
            try
            {
                // TODO: Replace with AutoMapper
                User user = new()
                {
                    FirstName = registerDTO.FirstName != null ? registerDTO.FirstName : "",
                    LastName = registerDTO.LastName != null ? registerDTO.LastName : "",
                    Email = registerDTO.Email,
                    UserName = registerDTO.UserName,
                    PhoneNumber = registerDTO.PhoneNumber != null ? registerDTO.PhoneNumber : "",
                    //PasswordHash = registerDTO.Password
                };
                return user;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new ArgumentException("Invalid Json passed to request body");
            }
        }

        private async Task<string> GenerateEmailConfirmationToken(User user)
        {
            try
            {
                var userId = await _userManager.GetUserIdAsync(user);
                return userId;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw new Exception("Error generating email confirmation");
            }
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {

            // Validate DTO
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            User user;

            // Map DTO to User entity
            try
            {
                user = ApplyTransformation(registerDTO);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            // Start registration process
            try
            {
                var result = await _userManager.CreateAsync(user, registerDTO.Password);          
                if (!result.Succeeded)
                {
                    Console.WriteLine(result);
                    return Problem("Registration failed...", statusCode: 500);
                }

                // Add user roles
                await _userManager.AddToRolesAsync(user, registerDTO.Roles);
                return Accepted();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Problem($"Something went wrong: {nameof(Register)}", statusCode: 500);
            }
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            // Validate DTO
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            // Start login process
            try
            {
                var result = await _signInManager.PasswordSignInAsync(loginDTO.UserName, loginDTO.Password, false, false);
                if (!result.Succeeded)
                {
                    Console.WriteLine(result);
                    if (result.IsNotAllowed)
                    {
                        return Unauthorized("Permission denied while accessing resource");
                    }
                    return Problem("Login failed", statusCode: 500);
                }
               return Accepted("Login success");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Problem($"Something went wrong: {nameof(Register)}", statusCode: 500);
            }
        }


        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return Accepted("User logged out");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Problem("Error loggin out", statusCode: 500);
            }
        }

        [HttpPost]
        [Route("{id}/code")]
        [Description("Generate email confirmation code")]
        public async Task<IActionResult> GenerateEmailConfirmationToken(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // Get user

                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                {
                    return NoContent();
                }
                
                // Generate Code

                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                return Ok(code);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return Problem("Something went wrong", statusCode: 500);
            }
        }

    }
}
