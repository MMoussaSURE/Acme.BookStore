

using Acme.BookStore.Authors;
using System.Threading.Tasks;
using System;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace Acme.BookStore.Clients
{
    public class ClientManager : DomainService
    {
        private readonly IClientRepository _clientRepository;
        public ClientManager(IClientRepository clientRepository)
        {
                _clientRepository = clientRepository;
        }
        public async Task<Client> CreateAsync(string name,ClientType type)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var existingClient = await _clientRepository.FindByNameAsync(name);
            if (existingClient != null)
                throw new ClientAlreadyExistsException(name);

            return new Client(GuidGenerator.Create(), name, type);
        }
        public async Task ChangeNameAsync(Client client, string newName)
        {
            Check.NotNull(client, nameof(client));
            Check.NotNullOrWhiteSpace(newName, nameof(newName));

            var existingClient = await _clientRepository.FindByNameAsync(newName);
            if (existingClient != null && existingClient.Id != client.Id)
                throw new AuthorAlreadyExistsException(newName);

            client.ChangeName(newName);
        }
    }
}
