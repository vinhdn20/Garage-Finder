using DataAccess.DTO.Token;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services
{
    public static class Extension
    {
        public static bool IsValidEmail(this string email)
        {
            var valid = true;

            try
            {
                var emailAddress = new MailAddress(email);
            }
            catch
            {
                valid = false;
            }

            return valid;
        }

        public static bool IsValidPhone(this string phone)
        {
            Regex rx = new Regex(@"\b(0)(3|5|7|8|9)+([0-9]{8})\b");
            return rx.IsMatch(phone);   
        }

        public static bool IsValidLicensePlates(this string licensePlates)
        {
            Regex rx = new Regex(@"\b(\d{2}-\w\w(-|\s)\d{4,5})|(\d{2}\w(-|\s)\d{4,5})\b");
            return rx.IsMatch(licensePlates);
        }
    }
}
