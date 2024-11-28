
using System;
using Volo.Abp;

namespace Acme.BookStore.Clients
{
    public class ClientNotExistException : BusinessException
    {
        public ClientNotExistException(Guid id) : base(BookStoreDomainErrorCodes.ClientNotExist)
        {
            WithData("id", id);
        }
    }
}
