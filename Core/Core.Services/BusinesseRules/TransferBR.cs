using Core.Domain.DTOs;
using Core.Domain.Entities;
using Core.Domain.Enums;
using Core.Domain.Exceptions;
using Core.Domain.Interfaces;
using Core.Domain.Msgs;
using Core.Infrastructure.Events;
using Core.Infrastructure.Json;
using Core.Infrastructure.Repository;
using Core.Infrastructure.Repository.Interfaces;
using Core.Infrastructure.Util;
using System.Text.Json;

namespace Core.Services.BusinesseRules
{
    public class TransferBR : ITransferBR
    {
        public static event EventHandler<TransferEventArgs>? TransferCompleted;
        public readonly IUserRepository _userRepository;
        public readonly IAccountRepository _accountRepository;

        public TransferBR(IUserRepository userRepository, IAccountRepository accountRepository)
        {
            _userRepository = userRepository;
            _accountRepository = accountRepository;
        }

        public async Task PerformTransactionAsync(TransferDTO dto)
        {
            if (dto.TransferValue <= 0)
                throw new ApiException(ApiMsg.EX001);

            User payerUser = _userRepository.GetByTaxNumber(dto.PayerTaxNumber) ?? throw new ApiException(ApiMsg.EX005);

            ValidateUserCanMakeTransfers(payerUser, dto.TransferValue);

            User payeeUser = _userRepository.GetByTaxNumber(dto.PayeeTaxNumber) ?? throw new ApiException(ApiMsg.EX006);

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

        private  void ValidateUserCanMakeTransfers(User user, decimal transferTotalValue)
        {
            if (user.Type == UserType.CNPJ) throw new ApiException(ApiMsg.EX007);

            Account fromAccount = _accountRepository.GetAccountByUserId(user.Id);

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

                var authorizationResponse = JsonSerializer.Deserialize<AuthorizationJson>(responseBody);

                return (bool)(authorizationResponse?.Data.Authorization)!;
            }
            catch
            {
                return false;
            }
        }
    }
}
