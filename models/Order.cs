using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Product_Management_System.models
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }

        [ForeignKey("ProductId")]
        public Product product { get; set; } = default!;
        
        public Guid ProductId { get; set; }

        [ForeignKey("UserId")]
        public User user { get; set; } 
        public Guid UserId { get; set; }
        public DateTime Time { get; set; }= DateTime.Now;
        public DateTime Date { get; set; } = DateTime.Today;
    }
}
