using EmailNotifications.Application.Dtos;
using EmailNotifications.SharedLibrary.RabbitMQ;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmailNotifications.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailNotificationsController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public EmailNotificationsController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmailNotification([FromBody] EmailNotificationModel request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Request is not valid");
            }

            await _publishEndpoint.Publish<IEmailNotification>(new
            {
                ClientId = request.ClientId,
                TemplateId = request.TemplateId,
                Recipients = request.Recipients,
                MarketingData = request.MarketingData
            });

            return Ok();
        }

        //[HttpPost("bulk")]
        //public async Task<IActionResult> SendBulkEmails(List<SendEmailRequest> requests)
        //{
        //    var tasks = requests.Select(request =>
        //        _publishEndpoint.Publish<ISendEmail>(new
        //        {
        //            EmailId = Guid.NewGuid().ToString(),
        //            request.To,
        //            request.Subject,
        //            request.Body,
        //            request.IsHtml,
        //            ScheduledFor = DateTime.UtcNow
        //        })
        //    );

        //    await Task.WhenAll(tasks);

        //    _logger.LogInformation("Queued {Count} emails for processing", requests.Count);
        //    return Accepted(new { Message = $"{requests.Count} emails queued for processing" });
        //}
    }
}
