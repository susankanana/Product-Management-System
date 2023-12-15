using Microsoft.EntityFrameworkCore;
using Product_Management_System.Data;
using Product_Management_System.models;
using Product_Management_System.Services.IService;

namespace Product_Management_System.Services
{
    public class UserService : IUser
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<User> GetUserByEmail(string email)
        {
            return await _context.Users.Where(x => x.Email.ToLower() == email.ToLower()).FirstOrDefaultAsync();
        }

        public async Task<string> RegisterUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return "User added successfully";
        }
    }
}
