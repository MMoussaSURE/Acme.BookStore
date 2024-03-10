using Acme.BookStore.Clients;
using Acme.BookStore.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Identity;


namespace Acme.BookStore.Orders
{
    
    
    public class OrderAppService(IOrderRepository orderRepository, OrderManager orderManager, IdentityUserAppService identityUserAppService) : BookStoreAppService, IOrderAppService
    {

        private readonly OrderManager _orderManager = orderManager;
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IdentityUserAppService _identityUserAppService = identityUserAppService;


        [Authorize(Roles = "admin")]
        public async Task<PagedResultDto<OrderDto>> GetListAsync(GetOrderListDto input)
        {
            using (_orderRepository.DisableTracking())
            {
                var fullUser = await _identityUserAppService.FindByEmailAsync(CurrentUser.Email);
                if (input.Sorting.IsNullOrWhiteSpace())
                    input.Sorting = nameof(Order.CreationTime);

                var totalCount = !input.ClientId.ToString().IsNullOrWhiteSpace()
                               ? await _orderRepository.CountAsync(x => x.ClientId == input.ClientId)
                               : await _orderRepository.CountAsync();

                //Expression<Func<Order, object>>[] propertySelectors = { p => p.Lines };

                //var queryable = await _orderRepository.GetQueryableAsync();
                //queryable = queryable.WhereIf(!input.ClientId.ToString().IsNullOrWhiteSpace(), c => c.ClientId == input.ClientId)
                //                     .Include(c => c.Client)
                //                     .Include(c => c.Lines).ThenInclude(l => l.Product)
                //                     .OrderBy(o => o.CreationTime)
                //                     .Skip(input.SkipCount)
                //                     .Take(input.MaxResultCount);

                //var orders = await AsyncExecuter.ToListAsync(queryable);

                var orders = await _orderRepository.GetListAsync(input.SkipCount, input.MaxResultCount, input.Sorting, input.ClientId);

                return new PagedResultDto<OrderDto>(totalCount, ObjectMapper.Map<List<Order>, List<OrderDto>>(orders));
            }
        }

        public async Task<OrderDto> GetAsync(Guid id)
        {
            var order = await _orderRepository.GetAsync(id, includeDetails: true);
            return ObjectMapper.Map<Order, OrderDto>(order);
        }

        [Authorize(BookStorePermissions.Orders.Default)]
        public async Task<OrderDto> CreateAsync(CreateOrderDto input)
        {
            var order = await _orderManager.CreateAsync(input.ClientId, input.Lines.Select(c => new OrderLine { ProductId = c.ProductId, Count = c.Count }).ToList());
            order = await _orderRepository.InsertAsync(order);
            return ObjectMapper.Map<Order, OrderDto>(order);
        }


        [Authorize(Roles = "admin")]
        public async Task DeleteAsync(Guid id)
          => await _orderRepository.DeleteAsync(id);

    }
}
