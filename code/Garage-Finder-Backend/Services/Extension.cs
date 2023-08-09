using DataAccess.DTO.Token;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Services
{
    public static class Extension
    {
        public static TokenInfor GetTokenInfor(this ClaimsPrincipal User)
        {
            var jsonUser = User.FindFirstValue("user");
            var user = JsonConvert.DeserializeObject<TokenInfor>(jsonUser);
            return user;
        }
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

        public static string HmacSHA512(this string inputData, string key)
        {
            var hash = new StringBuilder();
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            byte[] inputBytes = Encoding.UTF8.GetBytes(inputData);
            using (var hmac = new HMACSHA512(keyBytes))
            {
                byte[] hashValue = hmac.ComputeHash(inputBytes);
                foreach (var theByte in hashValue)
                {
                    hash.Append(theByte.ToString("x2"));
                }
            }

            return hash.ToString();
        }

        public static string convertToUnSign3(this string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public static string convertToUTF8(this string s)
        {
            byte[] bytes = Encoding.Default.GetBytes(s);
            var newString = Encoding.UTF8.GetString(bytes);
            return newString;
        }
    }
}
