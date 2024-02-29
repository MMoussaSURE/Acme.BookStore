using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;

namespace Acme.BookStore.Identity
{

    public class MyEfCoreIdentityUserRepository : EfCoreIdentityUserRepository
    {
        public MyEfCoreIdentityUserRepository(IDbContextProvider<IIdentityDbContext> dbContextProvider) : base(dbContextProvider)
        {
            
        }
        public override async Task<IdentityUser> FindByNormalizedEmailAsync(string normalizedEmail, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            var identityUser = await base.FindByNormalizedEmailAsync(normalizedEmail, includeDetails, cancellationToken);
            return identityUser is null
                ? throw new AbpIdentityResultException(new Microsoft.AspNetCore.Identity.IdentityResult () )
                : identityUser;
        }
    }
}
