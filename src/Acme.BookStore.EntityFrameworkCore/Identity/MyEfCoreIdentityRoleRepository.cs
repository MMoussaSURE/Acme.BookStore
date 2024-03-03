using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;

namespace Acme.BookStore.Identity
{
    public class MyEfCoreIdentityRoleRepository : EfCoreIdentityRoleRepository
    {
        public MyEfCoreIdentityRoleRepository(IDbContextProvider<IIdentityDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        public override async Task<List<IdentityRole>> GetDefaultOnesAsync(bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            return await(await GetDbSetAsync())
           .IncludeDetails(includeDetails)
           .Where(r => r.IsDefault)
           .ToListAsync(GetCancellationToken(cancellationToken));
           // return base.GetDefaultOnesAsync(includeDetails, cancellationToken);
        }

    }
}
