using Microsoft.AspNetCore.Identity;
using System;

namespace Loans.Domain.Entities.Identity
{
    public class User : IdentityUser
    {
        public string Name { get; set; }

        public Role Role { get; set; }

        public DateTime TokenExpires { get; set; }
    }
}
