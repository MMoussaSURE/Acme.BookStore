
using Acme.BookStore.Products;
using System;

namespace Acme.BookStore.Orders
{
    public class OrderLineDto
    {
        public Guid Id { get; set; }
    
        public int Count { get; set; }
        public double UnitPrice { get; set; }
        public ProductDto Product { get; set; }
    }
}
