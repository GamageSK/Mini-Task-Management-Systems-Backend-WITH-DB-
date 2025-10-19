using TaskManagement_Backend.Models;
using TaskManagementAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace TaskManagement_Backend.Services
{
    public class UserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> RegisterUserAsync(User user)
        {
            // Check if mobile number already exists
            var existingUser = await _context.Users
                .FirstOrDefaultAsync(u => u.MobileNumber == user.MobileNumber);

            if (existingUser != null)
                return false;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.MobileNumber == loginRequest.MobileNumber
                                       && u.Password == loginRequest.Password);

            if (user == null)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = "Invalid mobile number or password"
                };
            }

            return new LoginResponse
            {
                Success = true,
                Message = "Login successful",
                User = user
            };
        }

        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}