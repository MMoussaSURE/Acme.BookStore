using Volo.Abp.Account;
using Volo.Abp.DependencyInjection;

namespace Acme.BookStore.Controllers
{

    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(AccountController))]
    public class MyAccountController : AccountController
    {
        public MyAccountController(IAccountAppService accountAppService) : base(accountAppService)
        {
        }
        
    }
}
