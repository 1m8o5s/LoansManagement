using System;

namespace Loans.Domain.Models
{
    public class TokenWithExpireDateModel
    {
        public string Token { get; set; }

        public DateTime ExpireTime { get; set; }
    }
}
