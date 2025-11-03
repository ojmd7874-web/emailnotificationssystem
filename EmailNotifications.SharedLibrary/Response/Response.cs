using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmailNotifications.SharedLibrary.Response
{
    public record Response(bool Success = false, string Message = null);
}
