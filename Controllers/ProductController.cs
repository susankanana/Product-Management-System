using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product_Management_System.models;
using Product_Management_System.models.Dtos;
using Product_Management_System.Services;
using Product_Management_System.Services.IService;

namespace Product_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _productService;
        private readonly IMapper _mapper;
        public ProductController(IProduct prd, IMapper mapper)
        {
            _productService = prd;
            _mapper = mapper;

        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<List<ProductResponseDto>>> GetAllProducts(int pageNumber = 2, int pageSize = 2) {

            var response = await _productService.GetAllProducts(pageNumber,pageSize);
            var products = _mapper.Map<List<ProductResponseDto>>(response);
            return Ok(products);
        }
        [HttpGet("filter")]
        [Authorize]
        public async Task<ActionResult<List<ProductResponseDto>>> FilterProducts(string productName, int? price = null)
        {
            
                var response = await _productService.FilterProducts(productName, price);
                var products = _mapper.Map<List<ProductResponseDto>>(response);

                return Ok(products);
            
        }

        
        [HttpPost]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult<string>> AddProduct(AddProductDto newProduct)
        {
            var _newproduct = _mapper.Map<Product>(newProduct);
            var response = await _productService.AddProduct(_newproduct);
            return Created($"api/product/{_newproduct.ProductId}", response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponseDto>> GetProduct(Guid id)
        {
            var response = await _productService.GetProduct(id);
            var product = _mapper.Map<ProductResponseDto>(response);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            return Ok(product);
        }
        [HttpPut("id")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult<string>> UpdateProduct(Guid id, AddProductDto updProduct){
            var product = await _productService.GetProduct(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            var _updProduct = _mapper.Map(updProduct, product);
            var response = await _productService.UpdateProduct(_updProduct);
            return Ok(response);
        }

        [HttpDelete("id")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<ActionResult<string>> DeleteProduct(Guid id)
        {
            var product = await _productService.GetProduct(id);
            if (product == null)
            {
                return NotFound("Product not found");
            }
            var response = await _productService.DeleteProduct(product);
            return Ok(response);


        }
    }
}
