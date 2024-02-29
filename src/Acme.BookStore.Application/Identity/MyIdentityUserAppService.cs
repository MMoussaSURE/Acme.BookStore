
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;

namespace Acme.BookStore.Identity
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IIdentityUserAppService), typeof(IdentityUserAppService), typeof(MyIdentityUserAppService))]
    public class MyIdentityUserAppService : IdentityUserAppService
    {
        public MyIdentityUserAppService(IdentityUserManager userManager, IIdentityUserRepository userRepository, IIdentityRoleRepository roleRepository, IOptions<IdentityOptions> identityOptions) : base(userManager, userRepository, roleRepository, identityOptions)
        {
        }
        public override async Task<IdentityUserDto> FindByEmailAsync(string email)
        {
            return await base.FindByEmailAsync(email);
        }
    }
}
