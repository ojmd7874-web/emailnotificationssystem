using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotifications.Application.Dtos
{
    public record TemplateRequest(
        int? Id,
        [Required] string Name,
        [Required] string EmailSubject,
        [Required] string EmailContent,
        bool IsHtmlContent
    );
}
