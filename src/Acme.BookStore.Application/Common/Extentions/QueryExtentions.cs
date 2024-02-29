using Volo.Abp.Domain.Entities;
using LinqKit;

namespace Acme.BookStore.Common.Extentions
{
    public class QueryExtentions <TEntity> where TEntity : Entity
    {
        private static ExpressionStarter<TEntity> predicate = PredicateBuilder.New<TEntity>(true);
       
    }
}
