using System;
using System.Collections.Generic;
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

    }
}
