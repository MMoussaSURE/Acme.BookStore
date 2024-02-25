
using Hangfire;
using System;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Emailing;

namespace Acme.BookStore.BackgroundJob
{
    [Queue("alpha")]
    public class EmailSendingJob : AsyncBackgroundJob<EmailSendingArgs>, ITransientDependency
    {
        private readonly IEmailSender _emailSender;

        public EmailSendingJob(IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        public override async Task ExecuteAsync(EmailSendingArgs args)
        {
            try
            {
                await _emailSender.SendAsync(args.EmailAddress, args.Subject, args.Body );
            }
            catch(Exception ex)
            {
                throw new Exception($"{ex.Message}");
            }

           
        }
    }
}
