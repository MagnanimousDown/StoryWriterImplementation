using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StoryWriter.Services;

namespace StoryWriter.Controllers
{
    /*
    [Route("api/[controller]")]
    [ApiController]
    public class StoryController : ControllerBase
    {
        private readonly SupabaseService _supabaseService;

        public StoryController(SupabaseService supabaseService)
        {
            _supabaseService = supabaseService;
        }

        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await _supabaseService.GetCategoriesAsync();
                return Ok(categories);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error fetching categories: {ex.Message}");
            }
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> GetStoryDetails(int id)
        {
            try
            {
                var storyDetails = await _supabaseService.GetStoryDetailsAsync(id);
                return Ok(storyDetails);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error fetching story details: {ex.Message}");
            }
        }
    
    }
    */
}
