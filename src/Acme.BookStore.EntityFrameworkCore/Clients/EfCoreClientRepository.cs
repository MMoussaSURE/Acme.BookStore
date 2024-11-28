using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Acme.BookStore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Acme.BookStore.Clients
{
    public class EfCoreClientRepository : EfCoreRepository<BookStoreDbContext, Client, Guid>, IClientRepository
    {
        public EfCoreClientRepository(IDbContextProvider<BookStoreDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Client> FindByNameAsync(string name)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.FirstOrDefaultAsync(client => client.Name == name);
        }

        public async Task<List<Client>> GetListAsync(int skipCount, int maxResultCount, string sorting, string filter = null)
        {
            var dbSet = await GetDbSetAsync();
            return await dbSet.WhereIf(!filter.IsNullOrWhiteSpace(), client => client.Name.Contains(filter) )
                              .OrderBy(sorting)
                              .Skip(skipCount)
                              .Take(maxResultCount)
                              .ToListAsync();
        }
    }
}
