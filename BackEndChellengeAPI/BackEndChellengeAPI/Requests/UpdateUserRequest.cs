using BackEndChellengeAPI.Requests.Base;
using Core.Domain.Msgs;
using System.ComponentModel.DataAnnotations;

namespace BackEndChellengeAPI.Requests
{
    public class UpdateUserRequest: BaseRequest
    {
        [Required(ErrorMessageResourceType = typeof(RequestMsg), ErrorMessageResourceName = "ERR004")]
        public required string NewEmail { get; set; }

        [Required(ErrorMessageResourceType = typeof(RequestMsg), ErrorMessageResourceName = "ERR004")]
        public required string NewName { get; set; }

        [Required(ErrorMessageResourceType = typeof(RequestMsg), ErrorMessageResourceName = "ERR004")]
        public required string NewPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(RequestMsg), ErrorMessageResourceName = "ERR004")]
        public required string NewTaxNumber { get; set; }
    }
}
