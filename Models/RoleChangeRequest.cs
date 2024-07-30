using System.ComponentModel.DataAnnotations;

namespace StoryWriter.Models
{
    [Supabase.Postgrest.Attributes.Table("rolechangerequests")]
    public class RoleChangeRequest
    {
        [Supabase.Postgrest.Attributes.PrimaryKey("id")]
        public int Id { get; set; }

        [Required]
        [Supabase.Postgrest.Attributes.Column("userid")]
        public int UserId { get; set; }

        [Required]
        [Supabase.Postgrest.Attributes.Column("status")]
        public string Status { get; set; }

        [Supabase.Postgrest.Attributes.Column("createdat")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
