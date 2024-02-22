using Volo.Abp.Application.Dtos;

namespace Acme.BookStore.Clients
{
    public class GetClientListDto : PagedAndSortedResultRequestDto
    {
        public string? Filter { get; set; }
    }
}
