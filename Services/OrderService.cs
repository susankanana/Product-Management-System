using Microsoft.EntityFrameworkCore;
using Product_Management_System.Data;
using Product_Management_System.models;
using Product_Management_System.Services.IService;

namespace Product_Management_System.Services
{
    public class OrderService : IOrder
    {
        ApplicationDbContext _context;
        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> CreateOrder(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return "Order added successfully";
        }

        public async Task<bool> DeleteOrder(Order order)
        {
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Order>> GetAllOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetOrder(Guid id)
        {
            return await _context.Orders.Where(x => x.OrderId == id).FirstOrDefaultAsync();
        }

        public async Task<string> UpdateOrder(Order order)
        {
            //_context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return "Order updated successfully";
        }
        public async Task<List<Order>> GetUserOrders(Guid userId)
        {
            var userOrders = await _context.Orders.Where(x => x.UserId == userId).ToListAsync();
            return userOrders;
        }
    }
}
