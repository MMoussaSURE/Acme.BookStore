

using Acme.BookStore.Clients;
using System;
using System.Collections.Generic;

namespace Acme.BookStore.Orders
{
    public class OrderDto
    {
        public Guid Id { get; set; }
        public double TotalAmount { get; set; }
        public virtual DateTime CreationTime { get; set; }
        public ClientDto Client { get; set; }
        public List<OrderLineDto> Lines { get; set; }


    }
}
