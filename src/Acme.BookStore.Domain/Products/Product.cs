using System;
using Volo.Abp.Domain.Entities.Auditing;


namespace Acme.BookStore.Products
{
    public class Product : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get;  set; }
        public double Price { get; set; }
        private Product()
        {
            /* This constructor is for deserialization / ORM purpose */
        }

        internal Product( Guid id, string name,double price): base(id)
        {
           Name = name;
           Price = price;
        }
      
       
    }
}
