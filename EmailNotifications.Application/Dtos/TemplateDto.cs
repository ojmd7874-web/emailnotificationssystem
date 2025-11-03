using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotifications.Application.Dtos
{
    public record TemplateDto(
        int Id,
        string Name,
        string EmailSubject,
        string EmailContent,
        bool IsHtmlContent
    );
}
