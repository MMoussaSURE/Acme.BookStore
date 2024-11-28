using Acme.BookStore.ValueObjects;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace Acme.BookStore.Clients
{
    public class Client : FullAuditedAggregateRoot<Guid>
    {
        public string Name { get; private set; }
        public ClientType Type { get; set; }
        public Address HomeAddress { get; set; }
        public Address BusinessAddress { get; set; }

        private Client()
        {
            /* This constructor is for deserialization / ORM purpose */
        }

        internal Client(Guid id, string name, ClientType type) : base(id)
        {
            SetName(name);
            Type = type;
        }

        internal Client ChangeName(string name)
        {
            SetName(name);
            return this;
        }

        private void SetName(string name)
        {
            Name = Check.NotNullOrWhiteSpace(
                name,
                nameof(name),
                maxLength: ClientConsts.MaxNameLength
            );
        }
    }
}
