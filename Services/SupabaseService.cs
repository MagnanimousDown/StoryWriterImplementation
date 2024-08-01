using Microsoft.Extensions.Configuration;
using StoryWriter.Models;
using Supabase;
using Supabase.Gotrue;
using System.Threading.Tasks;
using System.Linq;
using Supabase.Postgrest;
using System.Collections.Generic;
using BCrypt.Net;
using System;
using System.Text;

namespace StoryWriter.Services
{
    public class SupabaseService
    {
        private readonly Supabase.Client _supabaseClient;
        private readonly IConfiguration _configuration;

        // In-memory store for temporary user data
        private static readonly Dictionary<string, TempUserDetails> TempUserStore = new Dictionary<string, TempUserDetails>();

        public SupabaseService(IConfiguration configuration)
        {
            _configuration = configuration;
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
                var verificationToken = GenerateVerificationToken(email);
                await SendVerificationEmail(email, verificationToken);

                TempUserStore[email] = new TempUserDetails
                {
                    FirstName = firstName,
                    LastName = lastName,
                    HashedPassword = BCrypt.Net.BCrypt.HashPassword(password),
                    InterestedCategories = interestedCategories
                };

                return new StoryWriter.Models.User
                {
                    Email = email
                };
            }

            return null;
        }

        public async Task<Session> LoginUserAsync(string email, string password)
        {
            var user = await GetUserByEmailAsync(email);
            if (user != null && user.EmailConfirmed && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
            {
                try
                {
                    return await _supabaseClient.Auth.SignIn(email, password);
                }
                catch (Exception ex)
                {
                    // Log the exception details for debugging
                    Console.WriteLine($"Supabase login error: {ex.Message}");
                }
            }
            return null;
        }

        public async Task<StoryWriter.Models.User> GetUserByEmailAsync(string email)
        {
            var users = await _supabaseClient.From<StoryWriter.Models.User>().Where(u => u.Email == email).Get();
            return users.Models.FirstOrDefault();
        }

        public async Task<bool> VerifyUserEmailAsync(string token)
        {
            var email = VerifyToken(token);
            if (!string.IsNullOrEmpty(email) && TempUserStore.TryGetValue(email, out var userDetails))
            {
                var user = new StoryWriter.Models.User
                {
                    FirstName = userDetails.FirstName,
                    LastName = userDetails.LastName,
                    Email = email,
                    PasswordHash = userDetails.HashedPassword,
                    InterestedCategories = userDetails.InterestedCategories,
                    CreatedAt = DateTime.UtcNow,
                    RoleId = 1, // Assuming default role ID
                    EmailConfirmed = true
                };

                var users = new List<StoryWriter.Models.User> { user };
                await _supabaseClient.From<StoryWriter.Models.User>().Insert(users);

                TempUserStore.Remove(email);

                return true;
            }

            return false;
        }

        private string GenerateVerificationToken(string email)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(email));
        }

        private string VerifyToken(string token)
        {
            return Encoding.UTF8.GetString(Convert.FromBase64String(token));
        }

        private async Task SendVerificationEmail(string email, string token)
        {
            var verificationUrl = $"{_configuration["AppSettings:BaseUrl"]}/api/EmailVerification/verify?token={token}";
            var emailContent = $"Please verify your email by clicking the following link: {verificationUrl}";

            var emailService = new EmailService(_configuration);
            await emailService.SendEmailAsync(email, "Email Verification", emailContent);
        }

        public async Task<IEnumerable<Story>> GetDataAsync(string table)
        {
            var query = _supabaseClient.From<Story>().Select("*");
            var result = await query.Get();
            return result.Models;
        }
        /*
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var categories = await _supabaseClient.From<Category>().Get();
            return categories.Models;
        }*/

        // Temporary user store details
        private class TempUserDetails
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string HashedPassword { get; set; }
            public int[] InterestedCategories { get; set; }
        }
    }
}
