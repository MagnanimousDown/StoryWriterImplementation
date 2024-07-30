using Microsoft.Extensions.Configuration;
using StoryWriter.Models;
using Supabase;
using Supabase.Gotrue;
using System.Threading.Tasks;
using System.Linq;
using Supabase.Postgrest;
using System.Collections.Generic;
using BCrypt.Net;

namespace StoryWriter.Services
{
    public class SupabaseService
    {
        private readonly Supabase.Client _supabaseClient;

        public SupabaseService(IConfiguration configuration)
        {
            var url = configuration["Supabase:Url"];
            var key = configuration["Supabase:AnonKey"];
            var options = new SupabaseOptions { AutoConnectRealtime = true };

            _supabaseClient = new Supabase.Client(url, key, options);
            _supabaseClient.InitializeAsync().Wait();
        }

public async Task<StoryWriter.Models.User> RegisterUserAsync(string firstName, string lastName, string email, string password, int[] interestedCategories)
        {
            var session = await _supabaseClient.Auth.SignUp(email, password);
            if (session?.User != null)
            {
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
                var user = new StoryWriter.Models.User
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = session.User.Email,
                    PasswordHash = password, // Ensure this is a hashed password in a real application
                    InterestedCategories = interestedCategories,
                    CreatedAt = DateTime.UtcNow,
                    RoleId = 1 // Assuming default role ID
                };

                // Use a collection for Insert
                var users = new List<StoryWriter.Models.User> { user };
                await _supabaseClient.From<StoryWriter.Models.User>().Insert(users);
                return user;
            }

            return null;
        }

        public async Task<Session> LoginUserAsync(string email, string password)
        {
            var user = await GetUserByEmailAsync(email);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                return await _supabaseClient.Auth.SignIn(email, password);
            }
            return null;
        }

        public async Task<StoryWriter.Models.User> GetUserByEmailAsync(string email)
        {
            var users = await _supabaseClient.From<StoryWriter.Models.User>().Where(u => u.Email == email).Get();
            return users.Models.FirstOrDefault();
        }
    }
}
