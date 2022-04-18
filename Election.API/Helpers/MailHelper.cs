
using Election.API.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Net;
using System.Net.Mail;

namespace Election.API.Helpers
{
    public class MailHelper : IMailHelper
    {
        private readonly IConfiguration _configuration;

        public MailHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Response SendMail(string to, string subject, string body)
        {
            try
            {
                string smtp = _configuration["Mail:Smtp"];
                MailMessage Email = new MailMessage();
                using (SmtpClient client = new SmtpClient(smtp))
                {

                    client.Port = 587;
                    client.EnableSsl = true;
                    Email.From = new MailAddress(_configuration["Mail:From"], "Constituencies Project");
                    Email.To.Add(new MailAddress(to));
                    Email.Subject = subject;
                    Email.Body = body;
                    Email.IsBodyHtml = true;
                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.UseDefaultCredentials = false;
                    client.Credentials = new NetworkCredential(_configuration["Mail:From"], _configuration["Mail:Password"]);
                    System.Net.ServicePointManager.ServerCertificateValidationCallback = (object se, System.Security.Cryptography.X509Certificates.X509Certificate cert, System.Security.Cryptography.X509Certificates.X509Chain chain, System.Net.Security.SslPolicyErrors sslerror) => true;
                    client.Send(Email);
                    Email.Dispose();
                    client.Dispose();
                }

                return new Response { IsSuccess = true };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                    Result = ex
                };
            }
        }
    }
}
