using Core.Common.DTOs;
using Core.Common.Entities;
using Core.Common.Enums;
using Core.Common.Exceptions;
using Core.Common.Repository;
using Core.Common.Responses;
using Core.Common.Services;
using Core.Common.Util;
using Core.Common.Util.Msgs;
using System.Text.Json;

namespace Core.Common.BusinesseRules
{
    public class TransferBR
    {
        public static event EventHandler<TransferEventArgs>? TransferCompleted;

        public static async Task PerformTransactionAsync(TransferDTO dto)
        {
            if (dto.TransferValue <= 0)
                throw new ApiException(ApiMsg.EX001);

            User payerUser = new UserService().GetUserByTaxNumber(dto.PayerTaxNumber) ?? throw new ApiException(ApiMsg.EX005);

            ValidateUserCanMakeTransfers(payerUser, dto.TransferValue);

            User payeeUser = new UserService().GetUserByTaxNumber(dto.PayeeTaxNumber) ?? throw new ApiException(ApiMsg.EX006);

            bool isAuthorized = await IsTransferAuthorizedAsync();

            if (!isAuthorized)
                throw new ApiException(ApiMsg.EX004);

            new TransactionRepository().PerformTransaction(payerUser, payeeUser, dto.TransferValue);

            OnTransferCompleted(new TransferEventArgs
            {
                Payer = payerUser,
                Payee = payeeUser,
                Amount = dto.TransferValue,
                TransferDate = DateTime.Now
            });
        }

        private static void ValidateUserCanMakeTransfers(User user, decimal transferTotalValue)
        {
            if (user.Type == UserType.CNPJ) throw new ApiException(ApiMsg.EX007);

            Account fromAccount = new AccountRepository().GetAccountByUserId(user.Id);

            if (fromAccount.Balance < transferTotalValue) throw new ApiException(ApiMsg.EX008);
        }

        private static void OnTransferCompleted(TransferEventArgs e)
        {
            TransferCompleted?.Invoke(null, e);
        }

        private static async Task<bool> IsTransferAuthorizedAsync()
        {
            var requestUri = AppSettings.AuthorizeTransferURL;
            try
            {
                HttpClient _httpClient = new HttpClient();

                HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                var authorizationResponse = JsonSerializer.Deserialize<AuthorizationResponse>(responseBody);

                return (bool)(authorizationResponse?.Data.authorization);
            }
            catch
            {
                return false;
            }
        }
    }
}
