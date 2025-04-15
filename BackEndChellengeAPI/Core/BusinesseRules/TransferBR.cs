using Core.DTOs;
using Core.Entities;
using Core.Enums;
using Core.Exceptions;
using Core.Repository;
using Core.Responses;
using Core.Services;
using System.Text.Json;

namespace Core.BusinesseRules
{
    public static class TransferBR
    {
        public static async Task PerformTransactionAsync(TransferDTO dto)
        {
            try
            {
                if (dto.TransferValue <= 0)
                    throw new ApiException("The TransferValue must be greater than 0.");

                User payerUser = new UserService().GetUserByTaxNumber(dto.PayerTaxNumber) ?? throw new ApiException("There is no registered user with the FromTaxNumber provided.");

                ValidateUserCanMakeTransfers(payerUser, dto.TransferValue);

                User payeeUser = new UserService().GetUserByTaxNumber(dto.PayeeTaxNumber) ?? throw new ApiException("There is no registered user with the ToTaxNumber provided.");

                bool isAuthorized = await IsTransferAuthorizedAsync();

                if (!isAuthorized) 
                    throw new ApiException("Transaction not authorized.");

                new TransactionRepository().PerformTransaction(payerUser, payeeUser, dto.TransferValue);

                dto.PayeeEmail = payeeUser.Email;
            }
            catch
            {
                throw;
            }
        }

        private static void ValidateUserCanMakeTransfers(User user, decimal transferTotalValue)
        {
            if (user.Type == UserType.CNPJ) throw new ApiException("Users of type CNPJ cannot make transfers.");

            Account fromAccount = new AccountRepository().GetAccountByUserId(user.Id);

            if (fromAccount.Balance < transferTotalValue) throw new ApiException("The source account does not have sufficient funds for the transaction.");
        }

        private static async Task<bool> IsTransferAuthorizedAsync()
        {
            var requestUri = "https://util.devi.tools/api/v2/authorize";
            try
            {
                HttpClient _httpClient = new HttpClient();

                HttpResponseMessage response = await _httpClient.GetAsync(requestUri);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                var authorizationResponse = JsonSerializer.Deserialize<AuthorizationResponse>(responseBody);

                return (bool)(authorizationResponse?.Data.authorization);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Erro ao chamar o serviço autorizador: {ex.Message}");
                return false;
            }
        }
    }
}
