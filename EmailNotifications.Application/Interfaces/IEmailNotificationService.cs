using EmailNotifications.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotifications.Application.Interfaces
{
    public interface IEmailNotificationService
    {
        Task<bool> SendEmailFromQueueMessage(EmailNotificationModel model);
    }
}
