using System.ComponentModel.DataAnnotations;

namespace StoryWriter.Models
{
    [Supabase.Postgrest.Attributes.Table("notifications")]
    public class Notification
    {
        [Supabase.Postgrest.Attributes.PrimaryKey("id")]
        public int Id { get; set; }

        [Required]
        [Supabase.Postgrest.Attributes.Column("recipientid")]
        public int[] RecipientId { get; set; }

        [Required]
        [Supabase.Postgrest.Attributes.Column("storyid")]
        public int StoryId { get; set; }

        [Required]
        [Supabase.Postgrest.Attributes.Column("message")]
        public string Message { get; set; }

        [Required]
        [Supabase.Postgrest.Attributes.Column("type")]
        public string Type { get; set; }

        [Required]
        [Supabase.Postgrest.Attributes.Column("subject")]
        public string Subject { get; set; }

        [Supabase.Postgrest.Attributes.Column("createdat")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
