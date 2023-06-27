using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.EmailService
{
    public interface IEmailService
    {
        Task<bool> SendMailAsync(string to, string name, string subject, string htmlpart, string textpart = "");
    }
}
