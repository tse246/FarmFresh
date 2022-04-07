using FarmFresh.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace FarmFresh.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private IConfiguration _config;

        public AuthenticationService(IConfiguration config)
        {
            _config = config;
        }

        private SymmetricSecurityKey GetSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        }

        private SigningCredentials GetCredentials(SymmetricSecurityKey securityKey)
        {
            return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        }

        public string GenerateJwtToken(User user)
        {
            var securityKey = GetSecurityKey();
            var credentials = GetCredentials(securityKey);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Username)
            };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddMinutes(5),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public User AuthenticateUser(UserLogin userLogin)
        {
            using (var context = new Context())
            {
                return (from u in context.Users
                        where u.Username == userLogin.Username && u.Password == userLogin.Password
                        select new User
                        {
                            Id = u.Id,
                            Username = u.Username,
                            Password = u.Password
                        }).FirstOrDefault();
            }
        }

        public bool ValidateJwtToken(string token)
        {
            try
            {
                token = token.Replace("Bearer ", "");
                var securityKey = GetSecurityKey();

                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                TokenValidationParameters validation = new TokenValidationParameters()
                {
                    ValidAudience = _config["Jwt:Audience"],
                    ValidIssuer = _config["Jwt:Issuer"],
                    ValidateIssuer = true,
                    ValidateLifetime = true,
                    LifetimeValidator = CustomLifetimeValidator,
                    RequireExpirationTime = true,
                    IssuerSigningKey = securityKey,
                    ValidateIssuerSigningKey = true
                };
                SecurityToken tokenOut;
                ClaimsPrincipal principal = handler.ValidateToken(token, validation, out tokenOut);
            }
            catch
            {
                return false;
            }

            return true;
        }

        private bool CustomLifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken tokenToValidate, TokenValidationParameters @param)
        {
            if (expires != null)
            {
                return expires > DateTime.UtcNow;
            }
            return false;
        }
    }
}
