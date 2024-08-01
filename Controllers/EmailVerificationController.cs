using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoryWriter.Services;
using System;
using System.Threading.Tasks;

namespace StoryWriter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailVerificationController : ControllerBase
    {
        private readonly SupabaseService _supabaseService;

        public EmailVerificationController(SupabaseService supabaseService)
        {
            _supabaseService = supabaseService;
        }

        [HttpGet("verify")]
        public async Task<IActionResult> VerifyEmail(string token)
        {
            try
            {
                var result = await _supabaseService.VerifyUserEmailAsync(token);
                if (result)
                {
                    return Ok("Email verified and user record created.");
                }

                return BadRequest("Verification failed.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Verification error: {ex.Message}");
            }
        }
    }
}
