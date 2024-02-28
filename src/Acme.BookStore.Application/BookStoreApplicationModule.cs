using Volo.Abp.Account;
using Volo.Abp.AutoMapper;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Identity;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.TenantManagement;
using Volo.Abp.Caching.StackExchangeRedis;
using Volo.Abp.Data;
using Volo.Abp;
using StackExchange.Redis;
using Microsoft.Extensions.DependencyInjection;
using Acme.BookStore.Clients;


namespace Acme.BookStore;

[DependsOn(
    typeof(BookStoreDomainModule),
    typeof(AbpAccountApplicationModule),
    typeof(BookStoreApplicationContractsModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule)
    )]
[DependsOn(typeof(AbpLocalizationModule))]
[DependsOn(typeof(AbpBackgroundJobsModule))]
[DependsOn(typeof(AbpCachingStackExchangeRedisModule))]
public class BookStoreApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
     //   context.Services.AddScoped<IClientAppService, ClientAppService>();
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<BookStoreApplicationModule>();
        });
        Configure<AbpBackgroundJobWorkerOptions>(options =>
        {
            options.DefaultTimeout = 864000; //10 days (as seconds)
        });

        //The code below disables the ISoftDelete filter by default which will cause to include deleted entities when you query the database unless you explicitly enable the filter:
        //Configure<AbpDataFilterOptions>(options =>
        //{
        //    options.DefaultStates[typeof(ISoftDelete)] = new DataFilterState(isEnabled: false);
        //});

    }


}
