using System.ComponentModel.DataAnnotations;

namespace StoryWriter.Models
{
    [Supabase.Postgrest.Attributes.Table("storypublishrequests")]
    public class StoryPublishRequest
    {
        [Supabase.Postgrest.Attributes.PrimaryKey("id")]
        public int Id { get; set; }

        [Required]
        [Supabase.Postgrest.Attributes.Column("storyid")]
        public int StoryId { get; set; }

        [Required]
        [Supabase.Postgrest.Attributes.Column("requestedbyuserid")]
        public int RequestedByUserId { get; set; }

        [Required]
        [Supabase.Postgrest.Attributes.Column("status")]
        public string Status { get; set; }

        [Supabase.Postgrest.Attributes.Column("createdat")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
