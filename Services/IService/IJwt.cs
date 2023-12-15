using Product_Management_System.models;

namespace Product_Management_System.Services.IService
{
    public interface IJwt
    {
        string GenerateToken(User user);
    }
}
