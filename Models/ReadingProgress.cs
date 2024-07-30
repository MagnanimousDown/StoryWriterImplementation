using System.ComponentModel.DataAnnotations;

namespace StoryWriter.Models
{
    [Supabase.Postgrest.Attributes.Table("readingprogress")]
    public class ReadingProgress
    {
        [Supabase.Postgrest.Attributes.PrimaryKey("id")]
        public int Id { get; set; }

        [Required]
        [Supabase.Postgrest.Attributes.Column("userid")]
        public int UserId { get; set; }

        [Required]
        [Supabase.Postgrest.Attributes.Column("storyid")]
        public int StoryId { get; set; }

        [Supabase.Postgrest.Attributes.Column("lastreadchapterid")]
        public int LastReadChapterId { get; set; }

        [Supabase.Postgrest.Attributes.Column("lastreadat")]
        public DateTime LastReadAt { get; set; }
    }
}
