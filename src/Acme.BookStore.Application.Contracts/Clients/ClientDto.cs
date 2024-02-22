using System;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Clients
{
    public class ClientDto : EntityDto<Guid>
    {
        public string Name { get; set; }
        public ClientType Type { get; set; }
    }
}
