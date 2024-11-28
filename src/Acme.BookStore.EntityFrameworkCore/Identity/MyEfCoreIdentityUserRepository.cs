using System;
using System.Collections.Generic;
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
            return await base.FindByNormalizedEmailAsync(normalizedEmail, includeDetails, cancellationToken);
        }

        public override Task<List<IdentityUser>> GetListAsync(string sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, string filter = null, bool includeDetails = false, Guid? roleId = null, Guid? organizationUnitId = null, string userName = null, string phoneNumber = null, string emailAddress = null, string name = null, string surname = null, bool? isLockedOut = null, bool? notActive = null, bool? emailConfirmed = null, bool? isExternal = null, DateTime? maxCreationTime = null, DateTime? minCreationTime = null, DateTime? maxModifitionTime = null, DateTime? minModifitionTime = null, CancellationToken cancellationToken = default)
        {
            return base.GetListAsync(sorting, maxResultCount, skipCount, filter, includeDetails, roleId, organizationUnitId, userName, phoneNumber, emailAddress, name, surname, isLockedOut, notActive, emailConfirmed, isExternal, maxCreationTime, minCreationTime, maxModifitionTime, minModifitionTime, cancellationToken);
        }
    }
}
