using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Acme.BookStore.Clients;
using Acme.BookStore.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
namespace Acme.BookStore.Orders
{
    public class EfCoreOrderRepository : EfCoreRepository<BookStoreDbContext, Order, Guid>, IOrderRepository
    {
        public EfCoreOrderRepository(IDbContextProvider<BookStoreDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
        public async Task<List<Order>> GetListAsync(int skipCount, int maxResultCount, string sorting, Guid? clientId = null)
        {

            var dbSet = await GetDbSetAsync();
            return await dbSet.WhereIf(!clientId.ToString().IsNullOrWhiteSpace(), c => c.ClientId == clientId)
                              .Include(c => c.Client)
                              .Include(l => l.Lines).ThenInclude(p => p.Product)
                              .OrderBy(sorting)
                              .Skip(skipCount)
                              .Take(maxResultCount)
                              .ToListAsync();
        }
    }
}
