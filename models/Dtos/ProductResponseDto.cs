﻿namespace Product_Management_System.models.Dtos
{
    public class ProductResponseDto
    {
        public Guid ProductId { get; set; } 
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int Price { get; set; }
    }
}
