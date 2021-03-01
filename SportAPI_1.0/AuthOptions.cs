using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SportAPI
{
    public class AuthOptions
    {
        public const string ISSUER = "SportServer"; // издатель токена
        public const string AUDIENCE = "SportPAIClient"; // потребитель токена
        const string KEY = "sportapiseasccascascascascsa!123";   // ключ для шифрации
        public const int LIFETIME = 1000; // время жизни токена - 1 минута
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
