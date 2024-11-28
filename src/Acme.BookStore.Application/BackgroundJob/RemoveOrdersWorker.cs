

using Acme.BookStore.Orders;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers.Hangfire;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Repositories;

namespace Acme.BookStore.BackgroundJob
{
    public class RemoveOrdersWorker : HangfireBackgroundWorkerBase
    {
        //private readonly ICachedServiceProvider _serviceProvider;
        private readonly IOrderRepository _orderRepository;

       // public RemoveOrdersWorker(ICachedServiceProvider serviceProvider)
        public RemoveOrdersWorker(IOrderRepository orderRepository)
        {
            RecurringJobId = nameof(RemoveOrdersWorker);
            CronExpression = Cron.Minutely();
            //_serviceProvider = serviceProvider;
            _orderRepository = orderRepository;
        }
        public override async Task DoWorkAsync(CancellationToken cancellationToken = default)
        {
            //using (var scope = _serviceProvider.CreateScope())
            //{
                Logger.LogInformation($"Executed RemoveOrdersWorker..! at {DateTime.Now}");
                //var orderRepo = scope.ServiceProvider.GetService<IRepository<Order, Guid>>();
                //if (_orderRepository is not null) 
                // await _orderRepository.DeleteAsync(c => c.CreationTime < DateTime.Now, true, cancellationToken);
            //}
            
        }
    }
}
