using Product_Management_System.models;

namespace Product_Management_System.Services.IService
{
    public interface IOrder
    {
        Task<List<Order>> GetAllOrders();
        Task<Order> GetOrder(Guid id);
        Task<string> UpdateOrder(Order order);
        Task<bool> DeleteOrder(Order order);
        Task<string> CreateOrder(Order order);
        Task<List<Order>> GetUserOrders(Guid userId);
    }
}
