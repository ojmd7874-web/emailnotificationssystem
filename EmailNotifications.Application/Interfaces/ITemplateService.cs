using EmailNotifications.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotifications.Application.Interfaces
{
    public interface ITemplateService
    {
        Task<List<TemplateDto>> GetAllAsync();
        Task<TemplateDto?> GetAsync(int id);
        Task<TemplateDto> SaveAsync(TemplateRequest request);
    }
}
