

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Caching;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.Identity;
using Volo.Abp.Security.Claims;
using Volo.Abp.Settings;
using Volo.Abp.Threading;

namespace Acme.BookStore.Identity
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IdentityUserManager))]
    public class MyIdentityUserManager : IdentityUserManager
    {
        public MyIdentityUserManager(IdentityUserStore store, IIdentityRoleRepository roleRepository, IIdentityUserRepository userRepository, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<IdentityUser> passwordHasher, IEnumerable<IUserValidator<IdentityUser>> userValidators, IEnumerable<IPasswordValidator<IdentityUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<IdentityUserManager> logger, ICancellationTokenProvider cancellationTokenProvider, IOrganizationUnitRepository organizationUnitRepository, ISettingProvider settingProvider, IDistributedEventBus distributedEventBus, IIdentityLinkUserRepository identityLinkUserRepository, IDistributedCache<AbpDynamicClaimCacheItem> dynamicClaimCache) : base(store, roleRepository, userRepository, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger, cancellationTokenProvider, organizationUnitRepository, settingProvider, distributedEventBus, identityLinkUserRepository, dynamicClaimCache)
        {
           
        }
        public override async Task<IdentityResult> AddDefaultRolesAsync(IdentityUser user)
        {
            await UserRepository.EnsureCollectionLoadedAsync(user, u => u.Roles, CancellationToken);

            foreach (var role in await RoleRepository.GetDefaultOnesAsync(cancellationToken: CancellationToken))
            {
                if (!user.IsInRole(role.Id))
                {
                    user.AddRole(role.Id);
                }
            }

            return await UpdateUserAsync(user);
          //  return await base.AddDefaultRolesAsync(user);
        }
        public override async Task<IdentityUser?> FindByEmailAsync(string email)
        {
            return await base.FindByEmailAsync(email);
        }
    }
}
