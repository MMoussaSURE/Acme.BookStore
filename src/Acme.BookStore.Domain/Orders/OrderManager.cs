
using Acme.BookStore.Clients;
using Acme.BookStore.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Acme.BookStore.Orders
{
    public class OrderManager(IRepository<Product, Guid> productRepository, IRepository<Client, Guid> clientRepository) : DomainService
    {

        private readonly IRepository<Client, Guid> _clientRepository = clientRepository;
        private readonly IRepository<Product, Guid> _productRepository = productRepository;

        public async Task<Order> CreateAsync(Guid clientId, List<OrderLine> lines)
        {
            if (lines is null || lines.Count == 0)
                throw new ArgumentException($"No Line for Current order");

            var client = await _clientRepository.GetAsync(clientId) ?? throw new ClientNotExistException(clientId);

            var orderProductIds = lines.DistinctBy(c=> c.ProductId).Select(c=> c.ProductId).ToList();

            var queryableProducts = await _productRepository.GetQueryableAsync();
            var notExistProductIds = queryableProducts.Where(c => orderProductIds.Contains(c.Id)).Count();
            if (notExistProductIds != orderProductIds.Count)
                throw new ProductNotExistException();


            var choosenProducts = await AsyncExecuter.ToListAsync(queryableProducts.Where(c => lines.Select(l => l.ProductId).ToList().Contains(c.Id)));

            var order = new Order(GuidGenerator.Create(), clientId,new List<OrderLine>());

            foreach (var line in lines)
            {
                order.Lines.Add(new OrderLine(GuidGenerator.Create() , line.ProductId,line.Count , choosenProducts.Where(f => f.Id == line.ProductId).Select(c => c.Price).FirstOrDefault()));
                order.TotalAmount += line.Count * choosenProducts.Where(f => f.Id == line.ProductId).Select(c => c.Price).FirstOrDefault();
            }
            
            return order;

        }


        
    }
}
