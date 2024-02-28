using System;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;


namespace Acme.BookStore.Products
{
    public class Product : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; private set; }
    }
}
