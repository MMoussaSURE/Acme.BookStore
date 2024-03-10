

using System;
using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Orders
{
    public class GetOrderListDto : PagedAndSortedResultRequestDto
    {
        public Guid? ClientId { get; set; }

    }
}
