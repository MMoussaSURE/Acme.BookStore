

using System;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Orders
{
    public class GetOrderListDto : PagedResultRequestDto
    {
        public Guid ClientId { get; set; }
    }
}
