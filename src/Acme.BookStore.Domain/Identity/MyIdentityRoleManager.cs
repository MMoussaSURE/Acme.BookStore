using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.Identity.Localization;
using Volo.Abp.Security.Claims;
using Volo.Abp.Threading;

namespace Acme.BookStore.Identity
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IdentityRoleManager))]
    public class MyIdentityRoleManager : IdentityRoleManager
    {
        public MyIdentityRoleManager(IdentityRoleStore store, IEnumerable<IRoleValidator<IdentityRole>> roleValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, ILogger<IdentityRoleManager> logger, IStringLocalizer<IdentityResource> localizer, ICancellationTokenProvider cancellationTokenProvider, IIdentityUserRepository userRepository, IOrganizationUnitRepository organizationUnitRepository, OrganizationUnitManager organizationUnitManager, IDistributedCache<AbpDynamicClaimCacheItem> dynamicClaimCache) : base(store, roleValidators, keyNormalizer, errors, logger, localizer, cancellationTokenProvider, userRepository, organizationUnitRepository, organizationUnitManager, dynamicClaimCache)
        {
        }    
    }
}
