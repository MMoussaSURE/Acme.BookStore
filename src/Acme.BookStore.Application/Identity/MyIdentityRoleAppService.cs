

using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;

namespace Acme.BookStore.Identity
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IIdentityRoleAppService), typeof(IdentityRoleAppService), typeof(MyIdentityRoleAppService))]
    public class MyIdentityRoleAppService : IdentityRoleAppService
    {
        public MyIdentityRoleAppService(IdentityRoleManager roleManager, IIdentityRoleRepository roleRepository) : base(roleManager, roleRepository)
        {
        }

       
    }
}
