﻿namespace Product_Management_System.models.Dtos
{
    public class OrderResponseDto
    {
        public Guid OrderId { get; set; }
        public string ProductId { get; set; }
        public string UserId { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        public DateTime Date { get; set; } = DateTime.Today;
    }
}
