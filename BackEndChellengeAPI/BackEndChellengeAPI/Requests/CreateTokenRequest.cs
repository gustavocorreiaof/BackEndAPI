using Core.Domain.Msgs;
using System.ComponentModel.DataAnnotations;

namespace BackEndChellengeAPI.Requests
{
    public class CreateTokenRequest
    {
        public required string cpf { get; set; }
        public required string senha { get; set; }
    }
}
