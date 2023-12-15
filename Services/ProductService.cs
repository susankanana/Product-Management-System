using Microsoft.EntityFrameworkCore;
using Product_Management_System.Data;
using Product_Management_System.models;
using Product_Management_System.Services.IService;

namespace Product_Management_System.Services
{
    public class ProductService : IProduct
    {
        private readonly ApplicationDbContext _context;
        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<string> AddProduct(Product product)
        {
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return "Product added successfully";
        }

        public async Task<bool> DeleteProduct(Product product)
        {
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Product>> GetAllProducts(int pageNumber, int pageSize)
        {
            //return await _context.Products.ToListAsync();
            var allProducts = _context.Products.ToListAsync();
            int itemsToSkip = (pageNumber - 1) * pageSize;
            var pagedProducts  = allProducts.Result.Skip(itemsToSkip).Take(pageSize).ToList();
            return pagedProducts;
        }
        public async Task<List<Product>> FilterProducts(string productName, int? price)
        {
            var allProducts = await _context.Products.ToListAsync();

            var filteredProducts = allProducts; //.Result

            // Apply filters based on the provided parameters
            if (!string.IsNullOrEmpty(productName))
            {
                filteredProducts = filteredProducts.Where(p => p.Name.Contains(productName, StringComparison.OrdinalIgnoreCase)).ToList();
            }

            if (price.HasValue)
            {
                filteredProducts = filteredProducts.Where(p => p.Price == price.Value).ToList();
            }

            return filteredProducts;
        }

        public async Task<Product> GetProduct(Guid id)
        {
            return await _context.Products.Where(x => x.ProductId == id).FirstOrDefaultAsync();
        }

        public async Task<string> UpdateProduct(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return "Product updated successfully";
        }
    }
}
