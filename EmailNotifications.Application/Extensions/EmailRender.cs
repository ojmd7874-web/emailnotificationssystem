using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotifications.Application.Extensions
{
    public static class EmailRender
    {
        public static string GenerateEmail(string template, Dictionary<string, string> data)
        {
            if (template == null)
            {
                return string.Empty;
            }
            if (data == null) 
            {
                return template;
            } 

            foreach (var item in data)
            {
                template = template.Replace("{{" + item.Key + "}}", item.Value ?? "");
            }

            return template;
        }
    }
}
