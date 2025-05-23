﻿using Core.Domain.Msgs;
using Core.Infrastructure.Events;
using Core.Infrastructure.Util;
using System.Net;
using System.Net.Mail;
using System.Text.Json;

namespace Core.Services.BusinesseRules
{
    public class SendEmailBR
    {
        private readonly int _smtpPort = AppSettings.SmtpPort;
        private readonly string _smtpServer = AppSettings.SmtpServer;
        private readonly string _senderEmail = AppSettings.SenderEmail;
        private readonly string _senderPassword = AppSettings.SenderPassword;
        private readonly string _senderName = AppSettings.SenderName;

        public void SendMailExecute(string msg)
        {
            TransferEventArgs json = JsonSerializer.Deserialize<TransferEventArgs>(msg);

            _ = SendEmailToPayeeAsync(json);
            _ = SendEmailToPayerAsync(json);
        }

        public async Task<bool> SendEmailToPayeeAsync(TransferEventArgs e)
        {
            try
            {
                var smtpClient = new SmtpClient(_smtpServer)
                {
                    Port = _smtpPort,
                    Credentials = new NetworkCredential(_senderEmail, _senderPassword),
                    EnableSsl = true
                };

                var mensagem = new MailMessage
                {
                    From = new MailAddress(_senderEmail, _senderName),
                    Subject = ApiMsg.INF003,
                    Body = string.Format(ApiMsg.INF004, e.Payee.Name, e.Payer.Name, e.Amount),
                    IsBodyHtml = false
                };

                mensagem.To.Add(e.Payee.Email);

                await smtpClient.SendMailAsync(mensagem);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> SendEmailToPayerAsync(TransferEventArgs e)
        {
            try
            {
                var smtpClient = new SmtpClient(_smtpServer)
                {
                    Port = _smtpPort,
                    Credentials = new NetworkCredential(_senderEmail, _senderPassword),
                    EnableSsl = true
                };

                var mensagem = new MailMessage
                {
                    From = new MailAddress(_senderEmail, _senderName),
                    Subject = ApiMsg.INF003,
                    Body = string.Format(ApiMsg.INF005, e.Amount, e.Payee.Name),
                    IsBodyHtml = false
                };

                mensagem.To.Add(e.Payer.Email);

                await smtpClient.SendMailAsync(mensagem);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
