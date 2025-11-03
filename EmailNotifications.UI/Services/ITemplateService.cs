using EmailNotifications.Application.Dtos;
using EmailNotifications.SharedLibrary.Response;

namespace EmailNotifications.UI.Services
{
    public interface ITemplateService
    {
        Task<List<TemplateDto>> GetAllTemplatesAsync();
        Task<TemplateDto?> GetTemplateAsync(int id);
        Task<TemplateDto?> SaveTemplateAsync(TemplateRequest request);
    }
}
