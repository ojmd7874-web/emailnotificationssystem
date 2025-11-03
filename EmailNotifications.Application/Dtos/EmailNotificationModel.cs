using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotifications.Application.Dtos
{
    public class EmailNotificationModel
    {
        [Required]
        public int ClientId { get; set; }

        [Required] 
        public int TemplateId { get; set; }

        [Required] 
        public List<string> Recipients { get; set; } = new List<string>();

        public Dictionary<string, string>? MarketingData { get; set; }
    }

}
