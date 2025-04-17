namespace Core.Common.DTOs
{
    public class TransferDTO
    {
        public TransferDTO(string payerTaxNumber, string payeeTaxNumber, decimal transferValue)
        {
            PayerTaxNumber = payerTaxNumber;
            PayeeTaxNumber = payeeTaxNumber;
            TransferValue = transferValue;
        }

        public string PayerTaxNumber { get; set; }

        public string PayeeTaxNumber { get; set; }

        public decimal TransferValue { get; set; }

        public string PayeeEmail { get; set; }
    }
}
