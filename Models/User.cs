using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace StoryWriter.Models
{
    [Supabase.Postgrest.Attributes.Table("users")]
    public class User : BaseModel
    {
        [Supabase.Postgrest.Attributes.PrimaryKey("id")]
        public int Id { get; set; }

        [Required]
        [Supabase.Postgrest.Attributes.Column("firstname")]
        public string FirstName { get; set; }
        
        [Required]
        [Supabase.Postgrest.Attributes.Column("lastname")]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [Supabase.Postgrest.Attributes.Column("email")]
        public string Email { get; set; }

        [Required]
        [Supabase.Postgrest.Attributes.Column("passwordhash")]
        public string PasswordHash { get; set; }

        [Supabase.Postgrest.Attributes.Column("createdat")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Supabase.Postgrest.Attributes.Column("roleid")]
        public int RoleId { get; set; } = 1;        //Keep RoleID for Reader Role as 1

        [Required]
        [Supabase.Postgrest.Attributes.Column("interestedcategories")]
        public int[] InterestedCategories { get; set; }

        [Required]
        [Supabase.Postgrest.Attributes.Column("emailconfirmed")]
        public bool EmailConfirmed { get; set; }

    }
}
