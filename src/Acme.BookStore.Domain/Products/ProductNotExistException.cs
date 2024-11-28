

using System;
using Volo.Abp;

namespace Acme.BookStore.Products
{
    public class ProductNotExistException  : BusinessException
    {
        public ProductNotExistException(): base(BookStoreDomainErrorCodes.ProductNotExist)
        {
           
        }
    }
}
