using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.Clients
{
    public interface IClientRepository : IRepository<Client, Guid>
    {
        Task<Client> FindByNameAsync(string name);
        Task<List<Client>> GetListAsync( int skipCount, int maxResultCount, string sorting, string filter = null );
    }
}
