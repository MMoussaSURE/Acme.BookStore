
using System;
using System.Collections.Generic;

namespace Acme.BookStore.Orders
{
    public class CreateOrderDto
    {
        public Guid ClientId { get; set; }
        public List<CreateOrderLineDto> Lines { get; set; } 
    }
}
