

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
        private readonly ICachedServiceProvider _serviceProvider;
       
        public RemoveOrdersWorker(ICachedServiceProvider serviceProvider)
        {
            RecurringJobId = nameof(RemoveOrdersWorker);
            CronExpression = Cron.Minutely();
            _serviceProvider = serviceProvider;
        }
        public override async Task DoWorkAsync(CancellationToken cancellationToken = default)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                Logger.LogInformation($"Executed RemoveOrdersWorker..! at {DateTime.Now}");
                var orderRepo = scope.ServiceProvider.GetService<IRepository<Order, Guid>>();
                if (orderRepo is not null) 
                 await orderRepo.DeleteAsync(c => c.CreationTime < DateTime.Now.AddDays(6),true);
            }
            
        }
    }
}
