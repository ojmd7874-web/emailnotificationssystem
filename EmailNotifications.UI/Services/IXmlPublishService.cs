namespace EmailNotifications.UI.Services
{
    public interface IXmlPublishService
    {
        Task<int> ParseAndPublishAsync(string xmlContent);
    }
}
