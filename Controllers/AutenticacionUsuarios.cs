using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Comercio.Controllers
{
    public class AutenticacionUsuarios
    {
        private readonly string SecretPassword = "7A26F4A905EB1F7F8395E68B4D5C32B2";

        public string GenerarToken(string nombreUsuario)
        {
            var clave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretPassword));
            var credenciales = new SigningCredentials(clave, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, nombreUsuario)
                
            };

            var token = new JwtSecurityToken(
                issuer: "Emisor",
                audience: "Destinatario",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1), 
                signingCredentials: credenciales
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public ClaimsPrincipal ValidarToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var clave = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretPassword));

            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = clave,
                    ValidateIssuer = true,
                    ValidIssuer = "Emisor",
                    ValidateAudience = true,
                    ValidAudience = "Destinatario",
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return (ClaimsPrincipal)tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = clave,
                    ValidateIssuer = true,
                    ValidIssuer = "Emisor",
                    ValidateAudience = true,
                    ValidAudience = "Destinatario",
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }, out validatedToken);
            }
            catch (Exception)
            {
            
                return null;
            }
        }
    }
}