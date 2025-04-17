using Core.Entities;
using Core.Util;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Core.BusinesseRules
{
    public class NotificationBR
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        private readonly string _smtpServer = AppSettings.SmtpServer;
        private readonly int _smtpPort = AppSettings.SmtpPort;
        private readonly string _senderEmail = AppSettings.SenderEmail;
        private readonly string _senderPassword = AppSettings.SenderPassword;
        private readonly string _senderName = AppSettings.SenderName;


        public async Task<bool> SendNotificationAsync(string to, string message)
        {
            var url = AppSettings.SendNotificationURL;

            var payload = new
            {
                to = to,
                message = message
            };

            var content = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(payload),
                Encoding.UTF8,
                "application/json"
            );

            try
            {
                var response = await _httpClient.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                    return false;

                return true;
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> SendEmailInternalAsync(TransferEventArgs e)
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
                    Subject = "Transferência Realizada",
                    Body = $"Olá {e.Payee.Name}, você recebeu uma transferência de {e.Payer.Name} no valor de R$ {e.Amount:F2}.",
                    IsBodyHtml = false
                };

                mensagem.To.Add(e.Payee.Email);

                await smtpClient.SendMailAsync(mensagem);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao enviar e-mail: {ex.Message}");
                return false;
            }
        }

        public void SendEmail(object? sender, TransferEventArgs e)
        {
            _ = SendEmailInternalAsync(e);
        }
    }
}
