using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Loans.Helpers.Identity
{
    public static class AuthenticationOptions
    {
        public const string ISSUER = "Loans";
        public const string AUDIENCE = "Loans";
        
        public const string SECRET_TOKEN = "NGZjMDI2c2Rmc2RmYWFhMmQtMGIyMy00MWEyMzEyMzEyMzktOTE0Mi03Nzc4ODM3ODE4ZjlkZmRkZHh6";
        public const int TIME = 15;

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SECRET_TOKEN));
        }
    }
}
