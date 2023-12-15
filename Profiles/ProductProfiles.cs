using AutoMapper;
using Product_Management_System.models;
using Product_Management_System.models.Dtos;

namespace Product_Management_System.Profiles
{
    public class ProductProfiles : Profile
    {
        public ProductProfiles()
        {
            CreateMap<AddProductDto, Product>().ReverseMap();
            CreateMap<AddOrderDto, Order>().ReverseMap();
            CreateMap<ProductResponseDto, Product>().ReverseMap();
            CreateMap<OrderResponseDto, Order>().ReverseMap();
            CreateMap<AddUserDto, User>().ReverseMap();
            CreateMap<UserOrderResponseDto,Product>().ReverseMap();
        }
    }
}
