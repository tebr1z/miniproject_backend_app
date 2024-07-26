﻿namespace Juan.Models
{
    public class ProductColor
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int ColorId { get; set; }
        public Color Color { get; set; }
        public int Quantity { get; set; }
    }

}
