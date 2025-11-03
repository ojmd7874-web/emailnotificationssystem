using EmailNotifications.Application.Dtos;
using EmailNotifications.Application.Extensions;
using EmailNotifications.Application.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotifications.Application.Services
{
    public class EmailNotificationService : IEmailNotificationService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ITemplateRepository _templateRepository;
        private readonly IConfiguration _configuration;
        public EmailNotificationService(IClientRepository clientRepository, ITemplateRepository templateRepository, IConfiguration configuration) 
        {
            _clientRepository = clientRepository;
            _templateRepository = templateRepository;
            _configuration = configuration;
        }

        public async Task<bool> SendEmailFromQueueMessage(EmailNotificationModel model)
        {
            var client = await _clientRepository.GetByIdAsync(model.ClientId);
            var template = await _templateRepository.GetByIdAsync(model.TemplateId);
            if (client == null || template == null) 
            {
                return false;
            }

            var renderedEmailContent = EmailRender.GenerateEmail(template.EmailContent, model.MarketingData);
            if (renderedEmailContent == null)
            {
                return false;
            }

            var emailMessage = new MailMessage
            {
                From = new MailAddress(client.Email),
                Subject = template.EmailSubject,
                Body = renderedEmailContent,
            };

            return true;

            //comment out because there is no actual email configuration set, if you want you can set it in appsettings.json
            //await SendMailAsync(emailMessage);
        }

        public async Task SendMailAsync(MailMessage message)
        {
            SmtpClient smtpClient = new(_configuration["MailServer:Host"])
            {
                Port = int.Parse(_configuration["MailServer:Host"]!),
                Credentials = new NetworkCredential
                {
                    UserName = _configuration["MailServer:Username"],
                    Password = _configuration["MailServer:Password"]
                },
                EnableSsl = bool.Parse(_configuration["MailServer:EnableSsl"]!)
            };

            message.From = new MailAddress(_configuration["MailServer:From"]!);

            await smtpClient.SendMailAsync(message);
        }
    }
}
