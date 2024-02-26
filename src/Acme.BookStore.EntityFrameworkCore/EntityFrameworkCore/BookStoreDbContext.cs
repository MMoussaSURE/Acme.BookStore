using Acme.BookStore.Authors;
using Acme.BookStore.Books;
using Acme.BookStore.Clients;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Linq.Expressions;
using System;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Acme.BookStore.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class BookStoreDbContext :
    AbpDbContext<BookStoreDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    #endregion

    #region Books
    public DbSet<Book> Books { get; set; }
    #endregion

    #region Authors
    public DbSet<Author> Authors { get; set; }
    #endregion

    #region Clients
    public DbSet<Client> Clients { get; set; }
    #endregion

    public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        builder.ConfigurePermissionManagement();
        builder.ConfigureSettingManagement();
        builder.ConfigureBackgroundJobs();
        builder.ConfigureAuditLogging();
        builder.ConfigureIdentity();
        builder.ConfigureOpenIddict();
        builder.ConfigureFeatureManagement();
        builder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(BookStoreConsts.DbTablePrefix + "YourEntities", BookStoreConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
        #region Books
        builder.Entity<Book>(b =>
        {
            b.ToTable(BookStoreConsts.DbTablePrefix + "Books", BookStoreConsts.DbSchema);
            b.ConfigureByConvention(); //auto configure for the base class props
            b.Property(x => x.Name).IsRequired().HasMaxLength(128);
            // ADD THE MAPPING FOR THE RELATION
            b.HasOne<Author>().WithMany().HasForeignKey(x => x.AuthorId).IsRequired();
            //b.HasAbpQueryFilter(c => c.IsActive);
        });
        #endregion

        #region Authors
        builder.Entity<Author>(b =>
        {
            b.ToTable(BookStoreConsts.DbTablePrefix + "Authors",
                BookStoreConsts.DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(AuthorConsts.MaxNameLength);

            b.HasIndex(x => x.Name);
        });
        #endregion

        #region Clients
        builder.Entity<Client>(b =>
        {
            b.ToTable(BookStoreConsts.DbTablePrefix + "Clients",
                BookStoreConsts.DbSchema);

            b.ConfigureByConvention();

            b.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(AuthorConsts.MaxNameLength);

            b.HasIndex(x => x.Name);
        });
        #endregion
    }


    //protected bool IsActiveFilterEnabled => DataFilter?.IsEnabled<IIsActive>() ?? false;

    //protected override bool ShouldFilterEntity<TEntity>(IMutableEntityType entityType)
    //{
    //    if (typeof(IIsActive).IsAssignableFrom(typeof(TEntity)))
    //    {
    //        return true;
    //    }

    //    return base.ShouldFilterEntity<TEntity>(entityType);
    //}

    //protected override Expression<Func<TEntity, bool>> CreateFilterExpression<TEntity>()
    //{
    //    var expression = base.CreateFilterExpression<TEntity>();

    //    if (typeof(IIsActive).IsAssignableFrom(typeof(TEntity)))
    //    {
    //        Expression<Func<TEntity, bool>> isActiveFilter =
    //            e => !IsActiveFilterEnabled || EF.Property<bool>(e, "IsActive");
    //        expression = expression == null
    //            ? isActiveFilter
    //            : QueryFilterExpressionHelper.CombineExpressions(expression, isActiveFilter);
    //    }

    //    return expression;
    //}

}
