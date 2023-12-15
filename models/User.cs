using System.ComponentModel.DataAnnotations;

namespace Product_Management_System.models
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;    
        public string Roles { get; set; } = "User";
        public List<Order> orders { get; set; }= new List<Order>();
    }
}
