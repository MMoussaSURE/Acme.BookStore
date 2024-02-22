using Acme.BookStore.Authors;
using Acme.BookStore.Books;
using Acme.BookStore.Clients;
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
    }
}
