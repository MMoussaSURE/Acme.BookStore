using System;
using Volo.Abp.Domain.Entities;

namespace Acme.BookStore.Orders
{
    public class OrderLine : Entity<Guid>
    {
        public Order Order { get; set; } //Navigation property
        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }
        public int Count { get; set; }
        public double UnitPrice { get; set; }
    }
}
