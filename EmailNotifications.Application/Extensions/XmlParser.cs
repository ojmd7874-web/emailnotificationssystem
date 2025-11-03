using EmailNotifications.Application.Dtos;
using System.Text.Json;
using System.Xml.Linq;

public static class XmlParser
{
    private static readonly JsonSerializerOptions _jsonOptions = new JsonSerializerOptions
    {
        PropertyNameCaseInsensitive = true
    };

    public static List<EmailNotificationModel> ParseFromXml(string xmlContent)
    {
        var emailNotifications = new List<EmailNotificationModel>();
        var doc = XDocument.Parse(xmlContent);

        foreach (var emailNotificationElement in doc.Descendants("EmailNotification"))
        {
            var model = new EmailNotificationModel
            {
                ClientId = int.Parse(emailNotificationElement.Element("ClientId")?.Value ?? "0"),
                TemplateId = int.Parse(emailNotificationElement.Element("TemplateId")?.Value ?? "0"),
                Recipients = emailNotificationElement.Element("Recipients")?
                    .Elements("Recipient")
                    .Select(e => e.Value)
                    .ToList() ?? new List<string>()
            };

            //if there is no clientid or templateid or no recipient don't parse the element
            if (model.ClientId == 0 || model.TemplateId == 0 || !model.Recipients.Any())
            {
                continue;
            }

            //don't parse the element if there is error in parsing the marketing data or there is no marketing data
            var marketingDataJson = emailNotificationElement.Element("MarketingData")?.Value;
            if (!string.IsNullOrEmpty(marketingDataJson))
            {
                try
                {
                    model.MarketingData = JsonSerializer.Deserialize<Dictionary<string, string>>(
                        marketingDataJson, _jsonOptions) ?? new Dictionary<string, string>();
                }
                catch (JsonException ex)
                {
                    Console.WriteLine($"Error parsing MarketingData JSON: {ex.Message}");
                    model.MarketingData = new Dictionary<string, string>();
                    continue;
                }
            }
            else
            {
                model.MarketingData = new Dictionary<string, string>();
                continue;
            }

            emailNotifications.Add(model);
        }

        return emailNotifications;
    }
}