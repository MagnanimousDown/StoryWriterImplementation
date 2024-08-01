using Supabase.Postgrest.Models;
using System.ComponentModel.DataAnnotations;

namespace StoryWriter.Models
{
    [Supabase.Postgrest.Attributes.Table("stories")]
    public class Story : BaseModel
    {
        [Supabase.Postgrest.Attributes.PrimaryKey("id")]
        public int Id { get; set; }

        [Required]
        [Supabase.Postgrest.Attributes.Column("title")]
        public string Title { get; set; }

        [Supabase.Postgrest.Attributes.Column("summary")]
        public string Summary { get; set; }

        [Required]
        [Supabase.Postgrest.Attributes.Column("status")]
        public string Status { get; set; }

        [Supabase.Postgrest.Attributes.Column("createdat")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Supabase.Postgrest.Attributes.Column("modifiedat")]
        public DateTime ModifiedAt { get; set; }

        [Required]
        [Supabase.Postgrest.Attributes.Column("userid")]
        public int UserId { get; set; }

        [Supabase.Postgrest.Attributes.Column("modifiedby")]
        public int ModifiedBy { get; set; }

        [Required]
        [Supabase.Postgrest.Attributes.Column("category")]
        public string Category { get; set; }

        [Supabase.Postgrest.Attributes.Column("collaborators")]
        public int[] Collaborators { get; set; }

        [Supabase.Postgrest.Attributes.Column("coverimage")]
        public string CoverImage { get; set; }

        [Supabase.Postgrest.Attributes.Column("previewimage")]
        public string PreviewImage { get; set; }
    }
}
