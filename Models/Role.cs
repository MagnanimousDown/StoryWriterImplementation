using System.ComponentModel.DataAnnotations;

namespace StoryWriter.Models
{
    [Supabase.Postgrest.Attributes.Table("roles")]
    public class Role
    {
        [Supabase.Postgrest.Attributes.PrimaryKey("id")]
        public int Id { get; set; }

        [Required]
        [Supabase.Postgrest.Attributes.Column("name")]
        public string Name { get; set; }
    }
}
