using EmailNotifications.Application.Dtos;
using EmailNotifications.SharedLibrary.RabbitMQ;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmailNotifications.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public AdminController(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        [HttpPost("load-xml")]
        public async Task<IActionResult> LoadXml([FromBody] LoadXmlRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid request");
            }

            List<EmailNotifications.Application.Dtos.EmailNotificationModel> items;
            try
            {
                items = XmlParser.ParseFromXml(request.Xml);
            }
            catch (System.Exception ex)
            {
                return BadRequest($"Invalid XML format: {ex.Message}");
            }

            if (items == null || !items.Any())
            {
                return BadRequest("No valid EmailNotification entries found in XML.");
            }

            var tasks = items.Select(item =>
                _publishEndpoint.Publish<IEmailNotification>(new
                {
                    ClientId = item.ClientId,
                    TemplateId = item.TemplateId,
                    Recipients = item.Recipients,
                    MarketingData = item.MarketingData
                })
            );

            await Task.WhenAll(tasks);

            return Ok(new { Count = items.Count });
        }
    }
}
