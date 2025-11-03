using EmailNotifications.Application.Interfaces;
using EmailNotifications.SharedLibrary.RabbitMQ;
using MassTransit;

namespace EmailNotifications.EmailQueueConsumer.Consumers
{
    public class EmailNotificationConsumer : IConsumer<IEmailNotification>
    {
        private readonly IEmailNotificationService _emailNotificationService;

        public EmailNotificationConsumer(IEmailNotificationService emailNotificationService)
        {
            _emailNotificationService = emailNotificationService;
        }

        public async Task Consume(ConsumeContext<IEmailNotification> context)
        {
            var message = context.Message;

            Console.WriteLine($"Email notification received: " +
                $"ClientId: {message.ClientId}, " +
                $"TemplateId: {message.TemplateId}, " +
                $"Recipients: {message.Recipients}, " +
                $"MarketingData: {message.MarketingData}");

            await _emailNotificationService.SendEmailFromQueueMessage(new Application.Dtos.EmailNotificationModel
            {
                ClientId = message.ClientId,
                TemplateId = message.TemplateId,
                Recipients = message.Recipients,
                MarketingData = message.MarketingData
            });
        }
    }
}
