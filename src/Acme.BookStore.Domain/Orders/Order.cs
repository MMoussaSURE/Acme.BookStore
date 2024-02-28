using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Orders
{
    public class Order : FullAuditedAggregateRoot<Guid>
    {
        public Guid ClientId { get; set; }

        public ICollection<OrderLine> Lines { get; set; } //Sub collection

        public Order()
        {
            Lines = new Collection<OrderLine>();
        }
    }
}
