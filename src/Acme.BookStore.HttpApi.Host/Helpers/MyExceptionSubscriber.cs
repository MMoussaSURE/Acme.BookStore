using Microsoft.Extensions.Logging;
using System.Buffers.Text;
using System.Threading.Tasks;
using Volo.Abp.ExceptionHandling;

namespace Acme.BookStore.Helpers
{
    public class MyExceptionSubscriber : ExceptionSubscriber
    {
        private readonly ILogger<MyExceptionSubscriber> _logger;
        public MyExceptionSubscriber(ILogger<MyExceptionSubscriber> logger)
        {
                _logger = logger;
        }
        public async override Task HandleAsync(ExceptionNotificationContext context)
        {
            var exception = context.Exception;
            var exceptionMessage = context.Exception.Message;
            _logger.LogException(exception,LogLevel.Error);

            // You can also perform any necessary actions based on the exception
            // Example: Sending an email notification to the administrator
            // Example: Notifying users about the error
        }
    }
}
