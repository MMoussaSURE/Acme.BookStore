

using Volo.Abp;

namespace Acme.BookStore.Clients
{
    public class ClientAlreadyExistsException : BusinessException
    {
        public ClientAlreadyExistsException(string name)
       : base(BookStoreDomainErrorCodes.ClientAlreadyExists)
        {
            WithData("name", name);
        }
    }
}
