using EmailNotifications.Application.Dtos;
using EmailNotifications.SharedLibrary.RabbitMQ;
using EmailNotifications.UI.Services;
using MassTransit;
using System.Text.Json;
using System.Xml;
using System.Xml.Linq;

namespace EmailNotifications.UI.Services;
public class XmlPublishService : IXmlPublishService
{
    private readonly IPublishEndpoint _publishEndpoint;

    public XmlPublishService(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    public async Task<int> ParseAndPublishAsync(string xmlContent)
    {
        if (string.IsNullOrEmpty(xmlContent))
            return 0;

        var publishedCount = 0;

        try
        {
            // Process elements sequentially as they're streamed from XML
            foreach (var notification in ParseEmailNotificationsStreaming(xmlContent))
            {
                // Publish each message immediately to RabbitMQ
                await _publishEndpoint.Publish<IEmailNotification>(new
                {
                    notification.ClientId,
                    notification.TemplateId,
                    notification.Recipients,
                    notification.MarketingData
                });

                publishedCount++;

                // Progress reporting
                if (publishedCount % 1000 == 0)
                {
                    Console.WriteLine($"Processed {publishedCount} messages...");
                }
            }

            Console.WriteLine($"Successfully published {publishedCount} messages");
            return publishedCount;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing XML: {ex.Message}");
            throw;
        }
    }

    private static IEnumerable<EmailNotificationModel> ParseEmailNotificationsStreaming(string xmlContent)
    {
        using var stringReader = new StringReader(xmlContent);
        using var xmlReader = XmlReader.Create(stringReader, new XmlReaderSettings
        {
            IgnoreWhitespace = true,
            IgnoreComments = true,
            DtdProcessing = DtdProcessing.Ignore
        });

        // Read through the XML document sequentially
        while (xmlReader.Read())
        {
            if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "EmailNotification")
            {
                var model = ParseEmailElementStreaming(xmlReader);
                if (model != null)
                {
                    yield return model;
                }
            }
        }
    }

    private static EmailNotificationModel? ParseEmailElementStreaming(XmlReader reader)
    {
        try
        {
            using var subReader = reader.ReadSubtree();
            var element = XElement.Load(subReader);
            return ParseEmailElement(element);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error parsing email element: {ex.Message}");
            return null;
        }
    }

    private static EmailNotificationModel? ParseEmailElement(XElement emailElement)
    {
        try
        {
            var clientIdElement = emailElement.Element("ClientId");
            var templateIdElement = emailElement.Element("TemplateId");
            var recipientsElement = emailElement.Element("Recipients");
            var marketingDataElement = emailElement.Element("MarketingData");

            // Validate required elements exist
            if (clientIdElement == null || templateIdElement == null ||
                recipientsElement == null || marketingDataElement == null)
            {
                Console.WriteLine("Missing required elements in EmailNotification");
                return null;
            }

            // Parse and validate ClientId
            if (!int.TryParse(clientIdElement.Value, out int clientId) || clientId <= 0)
            {
                Console.WriteLine($"Invalid ClientId: {clientIdElement.Value}");
                return null;
            }

            // Parse and validate TemplateId
            if (!int.TryParse(templateIdElement.Value, out int templateId) || templateId <= 0)
            {
                Console.WriteLine($"Invalid TemplateId: {templateIdElement.Value}");
                return null;
            }

            // Parse recipients
            var recipients = ParseRecipients(recipientsElement);
            if (recipients.Count == 0)
            {
                Console.WriteLine("No valid recipients found");
                return null;
            }

            // Parse marketing data
            var marketingData = ParseMarketingData(marketingDataElement.Value);
            if (marketingData == null || marketingData.Count == 0)
            {
                Console.WriteLine("Invalid or empty marketing data");
                return null;
            }

            return new EmailNotificationModel
            {
                ClientId = clientId,
                TemplateId = templateId,
                Recipients = recipients,
                MarketingData = marketingData
            };
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error parsing email element: {ex.Message}");
            return null;
        }
    }

    private static List<string> ParseRecipients(XElement recipientsElement)
    {
        var recipients = new List<string>();

        foreach (var recipientElement in recipientsElement.Elements("Recipient"))
        {
            recipients.Add(recipientElement.Value);
        }

        return recipients;
    }

    private static Dictionary<string, string>? ParseMarketingData(string marketingDataJson)
    {
        if (string.IsNullOrWhiteSpace(marketingDataJson))
        {
            Console.WriteLine("Marketing data JSON is null or empty");
            return null;
        }

        try
        {
            var marketingData = JsonSerializer.Deserialize<Dictionary<string, string>>(
                marketingDataJson,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return marketingData ?? new Dictionary<string, string>();
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"JSON parsing error in marketing data: {ex.Message}");
            return null;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error parsing marketing data: {ex.Message}");
            return null;
        }
    }
}
