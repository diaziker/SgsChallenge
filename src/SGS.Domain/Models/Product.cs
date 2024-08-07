﻿namespace SGS.Domain.Models
{
    public class Product
    {
        public string Id { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public Discount Discount { get; set; }
    }
}
