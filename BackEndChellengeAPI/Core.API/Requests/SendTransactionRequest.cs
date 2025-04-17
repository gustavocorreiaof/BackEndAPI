using Core.API.Requests.Attributes;
using Core.Common.Util.Msgs;
using System.ComponentModel.DataAnnotations;

namespace Core.API.Requests
{
    public class SendTransactionRequest
    {
        [Required(ErrorMessageResourceType = typeof(RequestMsg), ErrorMessageResourceName = "ERR003")]
        [CpfCnpjValidation]
        public string PayerTaxNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(RequestMsg), ErrorMessageResourceName = "ERR004")]
        [CpfCnpjValidation]
        public string PayeeTaxNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(RequestMsg), ErrorMessageResourceName = "ERR005")]
        public decimal TransferValue { get; set; }
    }
}
