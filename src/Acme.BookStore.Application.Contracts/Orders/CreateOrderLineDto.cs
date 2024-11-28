
using System;

namespace Acme.BookStore.Orders
{
    public class CreateOrderLineDto
    {
        public Guid ProductId { get; set; }
        public int Count { get; set; }
    }
}
