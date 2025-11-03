using EmailNotifications.SharedLibrary.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotifications.Domain.Entities
{
    public class Template : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string EmailSubject { get; set; }
        [Required]
        public string EmailContent { get; set; }
        public bool IsHtmlContent { get; set; }

    }
}
