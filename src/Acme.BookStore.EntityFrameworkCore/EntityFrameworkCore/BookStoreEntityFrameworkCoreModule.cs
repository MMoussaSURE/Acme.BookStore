using System;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Uow;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Acme.BookStore.Identity;
using Acme.BookStore.Orders;
using Volo.Abp.EntityFrameworkCore.DependencyInjection;

namespace Acme.BookStore.EntityFrameworkCore;

[DependsOn(
    typeof(BookStoreDomainModule),
    typeof(AbpIdentityEntityFrameworkCoreModule),
    typeof(AbpOpenIddictEntityFrameworkCoreModule),
    typeof(AbpPermissionManagementEntityFrameworkCoreModule),
    typeof(AbpSettingManagementEntityFrameworkCoreModule),
    typeof(AbpEntityFrameworkCoreSqlServerModule),
    typeof(AbpBackgroundJobsEntityFrameworkCoreModule),
    typeof(AbpAuditLoggingEntityFrameworkCoreModule),
    typeof(AbpTenantManagementEntityFrameworkCoreModule),
    typeof(AbpFeatureManagementEntityFrameworkCoreModule)
    )]
public class BookStoreEntityFrameworkCoreModule : AbpModule
{
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        BookStoreEfCoreEntityExtensionMappings.Configure();
    }

    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<BookStoreDbContext>(options =>
        {
                /* Remove "includeAllEntities: true" to create
                 * default repositories only for aggregate roots */
            options.AddDefaultRepositories(includeAllEntities: true);
        });

        // to use override identity repositories
        context.Services.AddDefaultRepository(typeof(Volo.Abp.Identity.IdentityUser), typeof(MyEfCoreIdentityUserRepository),replaceExisting: true);
        context.Services.AddDefaultRepository(typeof(Volo.Abp.Identity.IdentityUser), typeof(MyEfCoreIdentityUserRepository), replaceExisting: true);

        Configure<AbpDbContextOptions>(options =>
        {
            /* The main point to change your DBMS.
             * See also BookStoreMigrationsDbContextFactory for EF Core tooling. */
            options.UseSqlServer();
        });

        //Configure<AbpDbContextOptions>(options =>
        //{
        //    options.UseSqlServer(optionsBuilder =>
        //    {
        //        optionsBuilder.UseQuerySplittingBehavior(QuerySplittingBehavior.SingleQuery);
        //    });
        //});
        //Configure<AbpEntityOptions>(options =>
        //{
        //    options.Entity<Order>(orderOptions =>
        //    {
        //        orderOptions.DefaultWithDetailsFunc = query => query.Include(o => o.Lines).ThenInclude(p => p.Product);
        //    });
        //});

    }
}
