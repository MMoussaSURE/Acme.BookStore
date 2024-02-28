using Acme.BookStore.Clients;
using Acme.BookStore.Products;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Orders
{
    public class Order : FullAuditedAggregateRoot<Guid>
    {

        public Guid ClientId { get; set; }
        public Client Client { get; set; }
        public double TotalAmount { get; set; }
        public ICollection<OrderLine> Lines { get; set; } //Sub collection

        public Order()
        {
            Lines = new Collection<OrderLine>();
        }
       
        internal Order(Guid id, Guid clientId, ICollection<OrderLine> lines) : base(id)
        {
            ClientId = clientId;
            Lines = lines;
        }
    }
}
