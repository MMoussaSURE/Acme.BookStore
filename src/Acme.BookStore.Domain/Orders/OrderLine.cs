using Acme.BookStore.Products;
using System;
using Volo.Abp.Domain.Entities;

namespace Acme.BookStore.Orders
{
    public class OrderLine : Entity<Guid>
    {
        public Order Order { get; set; } //Navigation property
        public Guid OrderId { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public int Count { get; set; }
        public double UnitPrice { get; set; }

        public OrderLine()
        {

        }
        public OrderLine(Guid id,Guid productId,int count,double unitPrice) : base(id)
        {
            ProductId = productId;
            Count = count;
            UnitPrice = unitPrice;
        }
    }
}
