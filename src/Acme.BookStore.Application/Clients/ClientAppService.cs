using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Acme.BookStore.Permissions;
using Acme.BookStore.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;


namespace Acme.BookStore.Clients
{
    [Authorize(Roles = "admin ,Default")]
    [Authorize(BookStorePermissions.Clients.Default)]

    public class ClientAppService : BookStoreAppService, IClientAppService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ClientManager _clientManager;
        public ClientAppService(IClientRepository clientRepository, ClientManager clientManager)
        {
            _clientRepository = clientRepository;
            _clientManager = clientManager;
        }
        public async Task<ClientDto> GetAsync(Guid id)
        {
            var client = await _clientRepository.GetAsync(id);
            return ObjectMapper.Map<Client, ClientDto>(client);
        }
        public async Task<PagedResultDto<ClientDto>> GetListAsync(GetClientListDto input)
        {
            if (input.Sorting.IsNullOrWhiteSpace())
                input.Sorting = nameof(Client.Name);


            var clients = await _clientRepository.GetListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, input.Filter);

            var totalCount = input.Filter == null
                ? await _clientRepository.CountAsync()
                : await _clientRepository.CountAsync(author => author.Name.Contains(input.Filter));

            return new PagedResultDto<ClientDto>(totalCount, ObjectMapper.Map<List<Client>, List<ClientDto>>(clients));
        }

        [Authorize(BookStorePermissions.Clients.Create)]
        public async Task<ClientDto> CreateAsync(CreateClientDto input)
        {
            var client = await _clientManager.CreateAsync(input.Name, input.Type);
            client.HomeAddress = new Address(input.HomeAddress.Street, input.HomeAddress.City, input.HomeAddress.State, input.HomeAddress.Country, input.HomeAddress.ZipCode);
            client.BusinessAddress = new Address(input.BusinessAddress.Street, input.BusinessAddress.City, input.BusinessAddress.State, input.BusinessAddress.Country, input.BusinessAddress.ZipCode);


            await _clientRepository.InsertAsync(client);

            return ObjectMapper.Map<Client, ClientDto>(client);
        }

        [Authorize(BookStorePermissions.Clients.Edit)]
        public async Task UpdateAsync(Guid id, UpdateClientDto input)
        {
            var client = await _clientRepository.GetAsync(id);

            if (client.Name != input.Name)
                await _clientManager.ChangeNameAsync(client, input.Name);
            
            client.Type = input.Type;

             await _clientRepository.UpdateAsync(client);
        }
        [Authorize(BookStorePermissions.Clients.Delete)]
        public async Task DeleteAsync(Guid id)
        {
            await _clientRepository.DeleteAsync(id);
        }

    }
}
