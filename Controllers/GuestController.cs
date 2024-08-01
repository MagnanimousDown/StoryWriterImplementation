using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoryWriter.Services;

namespace StoryWriter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly SupabaseService _supabaseService;
        private readonly ILogger<GuestController> _logger;

        public GuestController(SupabaseService supabaseService, ILogger<GuestController> logger)
        {
            _supabaseService = supabaseService;
            _logger = logger;
        }

        [HttpGet("{table}")]
        public async Task<IActionResult> GetData(string table)
        {
            if (string.IsNullOrEmpty(table))
            {
                return BadRequest("Table name is required.");
            }



            try
            {
                //_logger.LogInformation($"Request received for table: {table} with columns: {string.Join(", ", columns)}");


                var data = await _supabaseService.GetDataAsync(table);

                return Ok(data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving data");
                return StatusCode(500, "Internal server error.");
            }
        }
    }
}
