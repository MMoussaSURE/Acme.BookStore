using Acme.BookStore.Clients;
using Acme.BookStore.Products;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace Acme.BookStore.Orders
{
    public class OrderAppService(IRepository<Order, Guid> orderRepository, OrderManager orderManager) : BookStoreAppService, IOrderAppService
    {

        private readonly OrderManager _orderManager = orderManager;
        private readonly IRepository<Order, Guid> _orderRepository = orderRepository;

        public async Task<OrderDto> CreateAsync(CreateOrderDto input)
        {
            var order = await _orderManager.CreateAsync(input.ClientId, input.Lines.Select(c => new OrderLine { ProductId = c.ProductId, Count = c.Count }).ToList());
            order = await _orderRepository.InsertAsync(order);
            return ObjectMapper.Map<Order, OrderDto>(order);
        }

        public async Task<PagedResultDto<OrderDto>> GetListAsync(GetOrderListDto input)
        {
            var totalCount = !input.ClientId.ToString().IsNullOrWhiteSpace()
                           ? await _orderRepository.CountAsync(x => x.ClientId == input.ClientId)
                           : await _orderRepository.CountAsync();
            Expression<Func<Order, object>>[] propertySelectors = {  p => p.Lines ,p => p.Client };

            var queryable = await _orderRepository.WithDetailsAsync(propertySelectors);
            queryable = queryable.WhereIf(!input.ClientId.ToString().IsNullOrWhiteSpace(), c => c.ClientId == input.ClientId)
                                 .OrderBy(o => o.CreationTime)
                                 .Skip(input.SkipCount)
                                 .Take(input.MaxResultCount);




            var orders = await AsyncExecuter.ToListAsync(queryable);
            return new PagedResultDto<OrderDto>(totalCount, ObjectMapper.Map<List<Order>, List<OrderDto>>(orders));
        }
    }
}
