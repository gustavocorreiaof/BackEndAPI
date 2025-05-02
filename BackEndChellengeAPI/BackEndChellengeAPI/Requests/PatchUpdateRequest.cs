using BackEndChellengeAPI.Requests.Base;
using Core.Domain.Msgs;
using System.ComponentModel.DataAnnotations;

namespace BackEndChellengeAPI.Requests
{
    public class PatchUpdateRequest : BaseRequest
    {
        [Required(ErrorMessageResourceType = typeof(RequestMsg), ErrorMessageResourceName = "ERR008")]
        public required string Value { get; set; }
    }
}
