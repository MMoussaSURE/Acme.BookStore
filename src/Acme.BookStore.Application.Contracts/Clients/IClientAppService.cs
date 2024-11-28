using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Acme.BookStore.Clients
{
    public interface IClientAppService : IApplicationService
    {
        Task<ClientDto> GetAsync(Guid id);

        Task<PagedResultDto<ClientDto>> GetListAsync(GetClientListDto input);

        Task<ClientDto> CreateAsync(CreateClientDto input);

        Task UpdateAsync(Guid id, UpdateClientDto input);

        Task DeleteAsync(Guid id);
    }
}
