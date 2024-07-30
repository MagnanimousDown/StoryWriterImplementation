using System.ComponentModel.DataAnnotations;

namespace StoryWriter.Models
{
    [Supabase.Postgrest.Attributes.Table("chapters")]
    public class Chapter
    {
        [Supabase.Postgrest.Attributes.PrimaryKey("id")]
        public int Id { get; set; }

        [Required]
        [Supabase.Postgrest.Attributes.Column("name")]
        public string Name { get; set; }

        [Required]
        [Supabase.Postgrest.Attributes.Column("content")]
        public string Content { get; set; }

        [Supabase.Postgrest.Attributes.Column("createdat")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Supabase.Postgrest.Attributes.Column("modifiedat")]
        public DateTime ModifiedAt { get; set; }

        [Required]
        [Supabase.Postgrest.Attributes.Column("storyid")]
        public int StoryId { get; set; }

        [Required]
        [Supabase.Postgrest.Attributes.Column("userid")]
        public int UserId { get; set; }

        [Required]
        [Supabase.Postgrest.Attributes.Column("modifiedby")]
        public int ModifiedBy { get; set; }
    }
}
