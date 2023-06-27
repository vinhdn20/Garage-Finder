using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using Mailjet.Client;
using Mailjet.Client.Resources;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Configuration;

namespace Services.EmailService
{
    public class MailjetService : IEmailService
    {
        private readonly IConfiguration _configuration;
        public MailjetService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<bool> SendMailAsync(string to, string name,string subject, string htmlpart, string textpart ="")
        {
            MailjetClient client = new MailjetClient(_configuration["Mailjet:APIKEY"], _configuration["Mailjet:APISECRET"]);
            MailjetRequest request = new MailjetRequest()
            {
                Resource = Send.Resource
            }
            .Property(Send.FromEmail, _configuration["Mailjet:FROMEMAIL"])
             .Property(Send.FromName, _configuration["Mailjet:FROMNAME"])
             .Property(Send.Recipients, new JArray {
                 new JObject {
                     {"Email", to},
                     {"Name", name}
                 }
             })
             .Property(Send.Subject, subject)
             .Property(Send.TextPart, textpart)
             .Property(Send.HtmlPart, htmlpart);
            MailjetResponse response = await client.PostAsync(request);
            if(response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
