using EmailNotifications.Application.Dtos;
using EmailNotifications.Application.Interfaces;
using EmailNotifications.Domain.Entities;

namespace EmailNotifications.Application.Services;

public class TemplateService : ITemplateService
{
    private readonly ITemplateRepository _templateRepository;

    public TemplateService(ITemplateRepository templateRepository)
    {
        _templateRepository = templateRepository;
    }

    public async Task<List<TemplateDto>> GetAllAsync()
    {
        var templates = await _templateRepository.GetAllAsync();
        return templates
            .Select(x => MapEntityToDto(x))
            .ToList();
    }

    public async Task<TemplateDto?> GetAsync(int id)
    {
        var template = await _templateRepository.GetByIdAsync(id);
        if (template == null)
        {
            return null;
        }

        return MapEntityToDto(template);
    }

    public async Task<TemplateDto> SaveAsync(TemplateRequest request)
    {
        if (request.Id.HasValue && request.Id.Value > 0)
        {
            var existingTemplate = await _templateRepository.GetByIdAsync(request.Id.Value);
            if (existingTemplate == null)
            {
                return null;
            }

            existingTemplate.Name = request.Name;
            existingTemplate.EmailSubject = request.EmailSubject;
            existingTemplate.EmailContent = request.EmailContent;
            existingTemplate.IsHtmlContent = request.IsHtmlContent;

            _templateRepository.Update(existingTemplate);
            await _templateRepository.SaveChangesAsync();

            return MapEntityToDto(existingTemplate);
        }
        else
        {
            var newTemplate = new Template
            {
                Name = request.Name,
                EmailSubject = request.EmailSubject,
                EmailContent = request.EmailContent,
                IsHtmlContent = request.IsHtmlContent
            };

            await _templateRepository.AddAsync(newTemplate);
            await _templateRepository.SaveChangesAsync();

            return MapEntityToDto(newTemplate);
        }
    }

    private static TemplateDto MapEntityToDto(Template entity)
    {
        return new TemplateDto(entity.Id, entity.Name, entity.EmailSubject, entity.EmailContent, entity.IsHtmlContent);
    }
}
