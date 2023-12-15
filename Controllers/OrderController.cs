using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product_Management_System.models;
using Product_Management_System.models.Dtos;
using Product_Management_System.Services.IService;
using System.Data;

namespace Product_Management_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IOrder _orderService;
        public OrderController(IMapper mapper, IOrder ord)
        {
            _mapper = mapper;
            _orderService = ord;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrderResponseDto>>> GetAllOrders()
        {
           var response = await _orderService.GetAllOrders();
           var orders = _mapper.Map<List<OrderResponseDto>>(response);
           return Ok(orders);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderResponseDto>> GetOrder(Guid id)
        {
            var response = await _orderService.GetOrder(id);
            var order = _mapper.Map<OrderResponseDto>(response);

            if(order== null) {
                return NotFound("Order was not found");
            }
            return Ok(order);
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<string>> AddOrder(AddOrderDto newOrder)
        {
            //var Id = User.Claims.FirstOrDefault(x => x.Type == "sub").Value;
            var list = User.Claims.ToList();
            var Id = list[1].Value;
            Console.WriteLine(Id);
            var _newOrder = _mapper.Map<Order>(newOrder);
            _newOrder.ProductId = new Guid(newOrder.ProductId);
            _newOrder.UserId = new Guid(newOrder.UserId);
            var response = await _orderService.CreateOrder(_newOrder);
            return Created($"api/order/{_newOrder.OrderId}", response);
        }
        [HttpPut("id")]
        public async Task<ActionResult> UpdateOrder(Guid id, AddOrderDto updOrder)
        {
            var order = await _orderService.GetOrder(id);
            if (order == null)
            {
                return NotFound("Order was not found");
            }
            var _updOrder = _mapper.Map(updOrder, order);
            var response = await _orderService.UpdateOrder(_updOrder);
            return Ok(response);
        }
        [HttpDelete("id")]
        public async Task<ActionResult<string>> DeleteProduct(Guid id)
        {
            var order = await _orderService.GetOrder(id);
            if (order == null)
            {
                return NotFound("Order was not found");
            }
            var response = await _orderService.DeleteOrder(order);
            return Ok(response);

        }
    }
}
