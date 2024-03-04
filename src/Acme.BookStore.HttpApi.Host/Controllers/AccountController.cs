using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Volo.Abp.Identity;
using Volo.Abp.Settings;
using System.Threading.Tasks;
using Volo.Abp.Account.Web.Areas.Account.Controllers.Models;
using Volo.Abp.DependencyInjection;


namespace Acme.BookStore.Controllers
{
   
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(Volo.Abp.Account.Web.Areas.Account.Controllers.AccountController),IncludeSelf =true)]

    public class AccountController : Volo.Abp.Account.Web.Areas.Account.Controllers.AccountController
    {
        public AccountController(SignInManager<Volo.Abp.Identity.IdentityUser> signInManager, IdentityUserManager userManager, ISettingProvider settingProvider, IdentitySecurityLogManager identitySecurityLogManager, IOptions<IdentityOptions> identityOptions, IdentityDynamicClaimsPrincipalContributorCache identityDynamicClaimsPrincipalContributorCache) : base(signInManager, userManager, settingProvider, identitySecurityLogManager, identityOptions, identityDynamicClaimsPrincipalContributorCache)
        {
        }

        [HttpPost]
        [Route("login")]
        public override async Task<AbpLoginResult> Login(Volo.Abp.Account.Web.Areas.Account.Controllers.Models.UserLoginInfo login)
        {
            return await base.Login(login);
        }
    }
}
