using Core.Domain.Msgs;
using System.ComponentModel.DataAnnotations;

namespace BackEndChellengeAPI.Requests.Base
{
    public class BaseRequest
    {
        [Required(ErrorMessageResourceType = typeof(RequestMsg), ErrorMessageResourceName = "ERR003")]
        public long UserId { get; set; }
    }
}
