namespace Acme.BookStore;

public static class BookStoreDomainErrorCodes
{
    #region Authors
    public const string AuthorAlreadyExists = "BookStore:00001";
    #endregion


    #region Clients
    public const string ClientAlreadyExists = "BookStore:00002";
    public const string ClientNotExist = "BookStore:000020";
    #endregion

    #region Products
    public const string ProductNotExist = "BookStore:00003";
    #endregion
}
