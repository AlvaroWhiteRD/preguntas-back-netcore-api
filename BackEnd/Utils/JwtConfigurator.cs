using BackEnd.Domain.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Utils
{
    public static class JwtConfirator
    {
        public static object JwtRegisteredClaimName { get; private set; }

        public static string GetToken(Users userInfo, IConfiguration configuration)
        {
            string SecretKey = configuration["jwt:SecretKey"];
            string Issuer = configuration["jwt:Issuer"];
            string Audience = configuration["jwt:Audience"];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.Username ),
                new Claim("userId", userInfo.Id.ToString())
            };

            var token = new JwtSecurityToken
            (
                issuer: Issuer,
                audience: Audience,
                claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        
        public static int GetTokenIdUser(ClaimsIdentity identity)
        {
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;

                foreach (var claim in claims)
                {
                    if (claim.Type == "userId")
                    {
                        return int.Parse( claim.Value);
                    }
                }
            }
            return 0;
        }
    }
}