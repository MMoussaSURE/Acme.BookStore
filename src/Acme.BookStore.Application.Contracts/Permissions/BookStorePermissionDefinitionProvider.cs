using Acme.BookStore.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Acme.BookStore.Permissions;

public class BookStorePermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var bookStoreGroup = context.AddGroup(BookStorePermissions.GroupName, L("Permission:BookStore"));

        #region Books
        var booksPermission = bookStoreGroup.AddPermission(BookStorePermissions.Books.Default, L("Permission:Books"));
        booksPermission.AddChild(BookStorePermissions.Books.Create, L("Permission:Books.Create"));
        booksPermission.AddChild(BookStorePermissions.Books.Edit, L("Permission:Books.Edit"));
        booksPermission.AddChild(BookStorePermissions.Books.Delete, L("Permission:Books.Delete"));
        #endregion

        #region Authors
        var authorsPermission = bookStoreGroup.AddPermission( BookStorePermissions.Authors.Default, L("Permission:Authors"));
        authorsPermission.AddChild(BookStorePermissions.Authors.Create, L("Permission:Authors.Create"));
        authorsPermission.AddChild(BookStorePermissions.Authors.Edit, L("Permission:Authors.Edit"));
        authorsPermission.AddChild(BookStorePermissions.Authors.Delete, L("Permission:Authors.Delete"));
        #endregion

        #region Clients
        var clientsPermission = bookStoreGroup.AddPermission(BookStorePermissions.Clients.Default, L("Permission:Clients"));
        clientsPermission.AddChild(BookStorePermissions.Clients.Create, L("Permission:Clients.Create"));
        clientsPermission.AddChild(BookStorePermissions.Clients.Edit, L("Permission:Clients.Edit"));
        clientsPermission.AddChild(BookStorePermissions.Clients.Delete, L("Permission:Clients.Delete"));
        #endregion


        #region Orders
        var ordersPermission = bookStoreGroup.AddPermission(BookStorePermissions.Orders.Default, L("Permission:Orders"));
        ordersPermission.AddChild(BookStorePermissions.Orders.Create, L("Permission:Orders.Create"));
        ordersPermission.AddChild(BookStorePermissions.Orders.Delete, L("Permission:Orders.Delete"));
        #endregion
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<BookStoreResource>(name);
    }
}
