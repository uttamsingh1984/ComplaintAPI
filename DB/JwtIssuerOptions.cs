using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace ComplantApi.DB
{
    public class JwtIssuerOptions
    {
        public string Issuer { get; set; }
        public string Subject { get; set; }
        public string Audience { get; set; }
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromMinutes(120);
        public DateTime IssuedAt => DateTime.UtcNow;
        public DateTime Expiration => IssuedAt.Add(ValidFor);
        public DateTime NotBefore => DateTime.UtcNow;
        public Func<Task<string>> JtiGenerator => () => Task.FromResult(Guid.NewGuid().ToString());
        public SigningCredentials SigningCredentials { get; set; }
    }    
}
