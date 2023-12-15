namespace Product_Management_System.models.Dtos
{
    public class UserOrderResponseDto
    {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public List<ProductResponseDto> Products { get; set; }=new List<ProductResponseDto>();
    }
}
