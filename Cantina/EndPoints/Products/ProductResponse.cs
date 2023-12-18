﻿using System.Xml.Linq;

namespace CantinaWebAPI.EndPoints.Products
{
    public class ProductResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string? Description { get; set; }
    }
}
