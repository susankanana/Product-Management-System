using Product_Management_System.models;

namespace Product_Management_System.Services.IService
{
    public interface IProduct
    {
        Task<List<Product>> GetAllProducts(int pageNumber, int pageSize);
        Task<List<Product>> FilterProducts(string productName, int? price);
        Task<Product> GetProduct(Guid id);
        Task<string> AddProduct(Product product);
        Task<string> UpdateProduct(Product product);
        Task<bool> DeleteProduct(Product product);


    }
}
