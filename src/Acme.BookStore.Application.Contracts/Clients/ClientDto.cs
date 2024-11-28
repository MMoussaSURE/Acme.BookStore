using Acme.BookStore.Common.Address;
using System;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Clients
{
    public class ClientDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public ClientType Type { get; set; }
        public virtual AddressDto HomeAddress { get; set; }
        public virtual AddressDto BusinessAddress { get; set; }
    }
}
