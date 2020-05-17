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
    public class AccountController: ControllerBase
    {
        private UserManager<AppUser> _userManager;        
        public AccountController(UserManager<AppUser> userManager)
        {
            _userManager=userManager;            
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Post(RegisterModel model){

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }            
            var userIdentity = new AppUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.Email
            };

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded)
                return BadRequest(ModelState);

            return Ok("Account created");
        }
    }
}