using System;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;

namespace Coditech.DataAccessLayer
{
    public abstract class BaseDataAccessLogic
    {
        public static string GenerateRandomPassword(int length =8)
        {
            const string lower = "abcdefghijklmnopqrstuvwxyz";
            const string upper = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            const string digits = "0123456789";
            const string special = "@$!%*?&#";

            string all = lower + upper + digits + special;
            var chars = new char[length];
            var rng = new RNGCryptoServiceProvider();
            byte[] data = new byte[1];

            int NextIndex(string set)
            {
                rng.GetBytes(data);
                return data[0] % set.Length;
            }

            chars[0] = lower[NextIndex(lower)];
            chars[1] = upper[NextIndex(upper)];
            chars[2] = digits[NextIndex(digits)];
            chars[3] = special[NextIndex(special)];

            for (int i = 4; i < length; i++)
                chars[i] = all[NextIndex(all)];

            return new string(chars.OrderBy(c => Guid.NewGuid()).ToArray());
        }
        private static void SendPasswordEmail(string toEmail, string password)
        {
            const string smtpHost = "smtp.your‑domain.com";
            const int smtpPort = 587;
            const string smtpUser = "no-reply@your-domain.com";
            const string smtpPass = "SMTP‑PASSWORD";

            var mail = new MailMessage
            {
                From = new MailAddress(smtpUser, "Coditech Support"),
                Subject = "Your Coditech Password",
                Body = $"Hello,\n\nYour password is: {password}\n\nPlease change it after logging in.\n\n– Coditech Team",
                IsBodyHtml = false
            };
            mail.To.Add(toEmail);

            using (var smtp = new SmtpClient(smtpHost, smtpPort)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(smtpUser, smtpPass)
            })
            {
                smtp.Send(mail); // 👈 synchronous call
            }
        }


    }
}

