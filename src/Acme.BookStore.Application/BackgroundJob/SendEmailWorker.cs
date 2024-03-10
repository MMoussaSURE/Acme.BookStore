using Hangfire;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.BackgroundWorkers.Hangfire;
using Volo.Abp.Emailing;

namespace Acme.BookStore.BackgroundJob
{
    public class SendEmailWorker : HangfireBackgroundWorkerBase
    {
        private readonly IEmailSender _emailSender;
        public SendEmailWorker(IEmailSender emailSender)
        {
            _emailSender = emailSender;
            RecurringJobId = nameof(SendEmailWorker);
            CronExpression = Cron.Minutely();
        }
        public override async Task DoWorkAsync(CancellationToken cancellationToken = default)
        {
            Logger.LogInformation($"Executed SendEmailWorker..! at {DateTime.Now}");
            await _emailSender.SendAsync("mmoussa@sure.com.sa", "test email sender", "hi mohamed ragab we test emaail sender", false, null);
           
        }
    }
}
