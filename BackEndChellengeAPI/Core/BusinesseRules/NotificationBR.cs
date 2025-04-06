using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BusinesseRules
{
    public class NotificationBR
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public async Task<bool> SendNotificationAsync(string to, string message)
        {
            var url = "https://util.devi.tools/api/v1/notify";

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
    }
}
