using Acme.BookStore.Authors;
using Acme.BookStore.Books;
using Acme.BookStore.Clients;
using Acme.BookStore.Common.Address;
using Acme.BookStore.Orders;
using Acme.BookStore.Products;
using Acme.BookStore.ValueObjects;
using AutoMapper;

namespace Acme.BookStore;

public class BookStoreApplicationAutoMapperProfile : Profile
{
    public BookStoreApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        #region Books
        CreateMap<Book, BookDto>();
        CreateMap<CreateUpdateBookDto, Book>();
        #endregion

        #region Authors
        CreateMap<Author, AuthorDto>();
        CreateMap<Author, AuthorLookupDto>();
        #endregion

        #region Clients
        CreateMap<Client, ClientDto>();
        #endregion

        #region Orders
        CreateMap<Order, OrderDto>();
        CreateMap<OrderLine, OrderLineDto>();
        #endregion

        #region Products
        CreateMap<Product, ProductDto>();
        #endregion

        #region Common
        CreateMap<Address, AddressDto>();
        #endregion
    }
}
