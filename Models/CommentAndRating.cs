using System.ComponentModel.DataAnnotations;

namespace StoryWriter.Models
{
    [Supabase.Postgrest.Attributes.Table("commentsandratings")]
    public class CommentAndRating
    {
        [Supabase.Postgrest.Attributes.PrimaryKey("id")]
        public int Id { get; set; }

        [Required]
        [Supabase.Postgrest.Attributes.Column("storyid")]
        public int StoryId { get; set; }

        [Required]
        [Supabase.Postgrest.Attributes.Column("userid")]
        public int UserId { get; set; }

        [Supabase.Postgrest.Attributes.Column("comment")]
        public string Comment { get; set; }

        [Supabase.Postgrest.Attributes.Column("rating")]
        public int Rating { get; set; }

        [Supabase.Postgrest.Attributes.Column("createdat")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Supabase.Postgrest.Attributes.Column("modifiedat")]
        public DateTime ModifiedAt { get; set; }
       
        [Supabase.Postgrest.Attributes.Column("deleted")]
        public bool Deleted { get; set; }


    }
}
