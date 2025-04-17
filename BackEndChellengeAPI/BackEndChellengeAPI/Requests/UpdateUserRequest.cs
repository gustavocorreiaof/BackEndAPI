using Core.Domain.Msgs;
using System.ComponentModel.DataAnnotations;

namespace BackEndChellengeAPI.Requests
{
    public class UpdateUserRequest
    {
        [Required(ErrorMessageResourceType = typeof(RequestMsg), ErrorMessageResourceName = "ERR003")]
        public long UserId { get; set; }

        [Required(ErrorMessageResourceType = typeof(RequestMsg), ErrorMessageResourceName = "ERR004")]
        public string NewEmail { get; set; }

        [Required(ErrorMessageResourceType = typeof(RequestMsg), ErrorMessageResourceName = "ERR004")]
        public string NewName { get; set; }

        [Required(ErrorMessageResourceType = typeof(RequestMsg), ErrorMessageResourceName = "ERR004")]
        public string NewPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(RequestMsg), ErrorMessageResourceName = "ERR004")]
        public string NewTaxNumber { get; set; }
    }
}
