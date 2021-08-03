using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BackEnd.Domain.IServices;
using BackEnd.Domain.Models;
using BackEnd.DTO;
using BackEnd.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Users user)
        {
            try
            {
                var validateExistence = await _usuarioService.ValidateExistence(user);
                if (validateExistence)
                {
                    return BadRequest(new { message ="El usuario " + user.Username + " ya existe!" } );
                }

                user.Password = EncryptedPassword.EncryptedPasswords(user.Password);
                await _usuarioService.SaveUser(user);

                return Ok(new { message = "Usuario registrado con exito!" });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [Route("changes-password")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        public async Task<IActionResult> CambiarPassword([FromBody] ChangesPasswordDTO changePassword)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;

                int idUsuario = JwtConfirator.GetTokenIdUser(identity);

                string encryptedPassword = EncryptedPassword.EncryptedPasswords(changePassword.BackPassword);
                var user = await _usuarioService.ValidatePassword(idUsuario, encryptedPassword);
                if(user == null)
                {
                    return BadRequest(new { message = "La contraseña es incrorrecta" });
                } else
                {
                    user.Password = EncryptedPassword.EncryptedPasswords(changePassword.NewPassword);
                    await _usuarioService.UpdatePassword(user);
                    return Ok(new { message = "La contraseña fue actualizada con exito!" });

                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
