using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotifications.Application.Dtos
{
    public record LoadXmlRequest(
        [Required] string Xml
    );
}
