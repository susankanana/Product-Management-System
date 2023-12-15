using Product_Management_System.models;

namespace Product_Management_System.Services.IService
{
    public interface IUser
    {
        Task<User> GetUserByEmail (string email);
        Task<string> RegisterUser (User user);
    }
}
