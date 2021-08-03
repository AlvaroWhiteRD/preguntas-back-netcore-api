using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.Domain.IServices;
using BackEnd.Domain.Models;
using BackEnd.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _loginService;
        private readonly IConfiguration _configuration;

        public LoginController(ILoginService loginService, IConfiguration configuration) 
        {
            _loginService = loginService;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Users user)
        {
            try
            {
                user.Password = EncryptedPassword.EncryptedPasswords(user.Password);
                var users = await _loginService.ValidateUser(user);
                if(users == null)
                {
                    return BadRequest(new { message = "Usuario o contraseña invalidos" });
                }
                string tokenJWT = JwtConfirator.GetToken(users, _configuration);
                return Ok(new { Token = tokenJWT });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
