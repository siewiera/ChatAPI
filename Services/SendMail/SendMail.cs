﻿using ChatAPI.Entities;
using ChatAPI.Interface;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;

namespace ChatAPI.Services.SendMail
{
    public class SendMail : ISendMail
    {
        //private string _configFilePath = "smtpConfig.json";
        private string _configFilePath = Path.Combine("Settings", "smtpConfig.json");
        //public SendMail(string configFilePath = "smtpConfig.json")
        //{
        //    _configFilePath = configFilePath;
        //}

        public void SetConfigFilePath(string configFilePath) 
        {
            _configFilePath = configFilePath;
        }

        private SmtpConfig LoadSmtpConfig()
        {        
            try
            {
                var jsonConfig = File.ReadAllText(_configFilePath);
                return JsonConvert.DeserializeObject<SmtpConfig>(jsonConfig);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }          
        }

        public void Send(string toAddress, string subject, string body)
        {
            var config = LoadSmtpConfig();

            try
            {
                using (var client = new SmtpClient(config.smtpServer, config.smtpPort))
                {
                    client.EnableSsl = config.useTls;
                    client.Credentials = new NetworkCredential(config.email, config.password);

                    MailMessage mailMessage = new MailMessage
                    {
                        From = new MailAddress(config.email),
                        Subject = subject,
                        Body = body,
                        IsBodyHtml = true,
                    };

                    mailMessage.To.Add(toAddress);
                    client.Send(mailMessage);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while sending the email: {ex.Message}");
            }

        }
    }
}
