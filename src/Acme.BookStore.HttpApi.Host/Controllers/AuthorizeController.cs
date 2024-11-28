

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Security;
using Volo.Abp.DependencyInjection;

namespace Acme.BookStore.Controllers
{
    [Route("connect/authorize")]
    [ApiExplorerSettings(IgnoreApi = false)]
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(Volo.Abp.OpenIddict.Controllers.AuthorizeController), IncludeSelf = true)]
    public class AuthorizeController : Volo.Abp.OpenIddict.Controllers.AuthorizeController
    {
        [HttpGet, HttpPost]
        [IgnoreAntiforgeryToken]
        [IgnoreAbpSecurityHeader]
        public override async Task<IActionResult> HandleAsync()
        {
            return await base.HandleAsync();
        }


        [HttpPost]
        [Authorize]
        [Route("callback")]
        public override async Task<IActionResult> HandleCallbackAsync()
        {
            return await base.HandleCallbackAsync();
        }
    }
}
