using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StoryWriter.Models;
using StoryWriter.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StoryWriter.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SupabaseService _supabaseService;

        public AuthController(IConfiguration configuration, SupabaseService supabaseService)
        {
            _configuration = configuration;
            _supabaseService = supabaseService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            try
            {
                var user = await _supabaseService.RegisterUserAsync(request.FirstName, request.LastName, request.Email, request.Password, request.InterestedCategories);
                if (user == null)
                {
                    return BadRequest("Registration failed.");
                }

                return Ok("User registered successfully. Please verify your email.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Registration error: {ex.Message}");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            try
            {
                var userSession = await _supabaseService.LoginUserAsync(request.Email, request.Password);
                if (userSession == null || userSession.AccessToken == null)
                {
                    return Unauthorized("Invalid credentials.");
                }

                var token = GenerateJwtToken(request.Email);
                return Ok(new { Token = token });
            }
            catch (Exception ex)
            {
                // Log the exception details for debugging
                Console.WriteLine($"Login error: {ex.Message}");
                return BadRequest($"Login error: {ex.Message}");
            }
        }

        private string GenerateJwtToken(string email)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    public class RegisterRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int[] InterestedCategories { get; set; }
    }

    public class LoginRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
