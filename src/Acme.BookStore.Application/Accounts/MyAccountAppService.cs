using Acme.BookStore.Identity;
using Acme.BookStore.Permissions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Account;
using Volo.Abp.Account.Emailing;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using Volo.Abp.ObjectExtending;
using Volo.Abp.PermissionManagement;

namespace Acme.BookStore.Accounts
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(IAccountAppService), typeof(AccountAppService), typeof(MyAccountAppService))]
    public class MyAccountAppService : AccountAppService
    {

        private readonly IPermissionManagementProvider _permissionManagementProvider;

        public MyAccountAppService(IdentityUserManager userManager, IIdentityRoleRepository roleRepository, IAccountEmailer accountEmailer, IdentitySecurityLogManager identitySecurityLogManager, IOptions<IdentityOptions> identityOptions, IPermissionManagementProvider permissionManagementProvider) : base(userManager, roleRepository, accountEmailer, identitySecurityLogManager, identityOptions)
        {

            _permissionManagementProvider = permissionManagementProvider;
        }
        public override async Task<IdentityUserDto> RegisterAsync(RegisterDto input)
        {

            await CheckSelfRegistrationAsync();

            await IdentityOptions.SetAsync();
        
            var user = new IdentityUser(GuidGenerator.Create(), input.UserName, input.EmailAddress, CurrentTenant.Id);

            input.MapExtraPropertiesTo(user);

            (await UserManager.CreateAsync(user, input.Password)).CheckErrors();

            await UserManager.SetEmailAsync(user, input.EmailAddress);
            await UserManager.AddDefaultRolesAsync(user);

            // add permission for users 
            await AddPermissionsForDefaultRole(user.Id.ToString());

            return ObjectMapper.Map<IdentityUser, IdentityUserDto>(user);
            // return await base.RegisterAsync(input);
        }


        private async Task<bool> AddPermissionsForDefaultRole(string providerKey )
        {
            var defaultPermissions = new List<string>
            {
                BookStorePermissions.Books.Default,
                BookStorePermissions.Authors.Default,
                BookStorePermissions.Orders.Default,
                BookStorePermissions.Orders.Create,
                BookStorePermissions.Orders.Default
            };

            foreach(var permission in defaultPermissions )
              await _permissionManagementProvider.SetAsync(permission, providerKey, true);

            
            return true;


        }


    }
}
