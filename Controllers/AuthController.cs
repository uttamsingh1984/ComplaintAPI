using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ComplantApi.DB;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Security.Principal;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;

namespace ComplaintApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController: ControllerBase
    {
        private UserManager<AppUser> _userManager;
        private JwtIssuerOptions _jwtIssuerOptions;
        public AuthController(UserManager<AppUser> userManager, IOptions<JwtIssuerOptions> jwtIssuerOptions)
        {
            _userManager=userManager;
            _jwtIssuerOptions= jwtIssuerOptions.Value;
        }

        [Route("login")]
        public async Task<IActionResult> Post(UserModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userName = model.UserName;
            var password = model.Password;

            var userToVerify = await _userManager.FindByNameAsync(userName);

            if (userToVerify == null)
                return BadRequest();

            // check the credentials
            if (await _userManager.CheckPasswordAsync(userToVerify, password))
            {                
                var identity = new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
                {
                    new Claim("id", userToVerify.Id),
                    new Claim("FirstName", userToVerify.FirstName),
                    new Claim("LastName", userToVerify.LastName),
                    new Claim("rol", "apiAccess")

                });

                var jwt = await GenerateJwt(identity, model.UserName, _jwtIssuerOptions,  new JsonSerializerSettings { Formatting = Formatting.Indented });
                return new OkObjectResult(jwt);
            }
            return BadRequest();
        }
        private async Task<string> GenerateJwt(ClaimsIdentity identity, string userName, JwtIssuerOptions _jwtOptions, JsonSerializerSettings serializerSettings)
        {            
            Claim []claims = {
                 new Claim(JwtRegisteredClaimNames.Sub, userName),
                 new Claim(JwtRegisteredClaimNames.Jti,  await _jwtOptions.JtiGenerator()),
                 new Claim(JwtRegisteredClaimNames.Iat, ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
                 identity.FindFirst("rol"),
                 identity.FindFirst("id"),
                 identity.FindFirst("FirstName"),
                 identity.FindFirst("LastName")
             };

            // Create the JWT security token and encode it.
            var jwt = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                notBefore: _jwtOptions.NotBefore,
                expires: _jwtOptions.Expiration,
                signingCredentials: _jwtOptions.SigningCredentials);

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);           

            var response = new
            {
                id = identity.Claims.Single(c => c.Type == "id").Value,
                auth_token = encodedJwt,
                expires_in = (int)_jwtOptions.ValidFor.TotalSeconds
            };

            return JsonConvert.SerializeObject(response, serializerSettings);
        }

        private long ToUnixEpochDate(DateTime date)
        => (long)Math.Round((date.ToUniversalTime() -
                             new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                            .TotalSeconds);
    }


}