using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotifications.SharedLibrary.RabbitMQ
{
    public interface IEmailNotification
    {
        public int ClientId { get; set; }

        public int TemplateId { get; set; }

        public List<string> Recipients { get; set; }

        public Dictionary<string, string>? MarketingData { get; set; }
    }
}
