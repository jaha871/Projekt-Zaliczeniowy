using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using PrywatnaPrzychodniaEntities;
using PrywatnaPrzychodniaEntities.Entities;
using PrywatnaPrzychodniaLekarska.Authorization;
using PrywatnaPrzychodniaLekarska.Contracts;
using PrywatnaPrzychodniaLekarska.Exeptions;
using PrywatnaPrzychodniaLekarska.Models;

namespace PrywatnaPrzychodniaLekarska.Services
{
    public class LoginService : ConnectionService, ILoginService
    {
        private IPasswordHasher<User> _passwordHasher;
        private AuthenticationOptions _authenticationSettings;

        public LoginService(PrywatnaPrzychodniaDbContext context, IPasswordHasher<User> passwordHasher, AuthenticationOptions authenticationSettings) : base(context)
        {
            _authenticationSettings = authenticationSettings;
            _passwordHasher = passwordHasher;
        }

        public AuthResult TokenGenerator(Credentials dto)
        {
            var user = _context.Users
                ?.FirstOrDefault(u => u.Email.ToLower() == dto.Email.ToLower());

            if (user is null)
            {
                throw new NotFoundException("Użytkownik nie intnieje.");
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, dto.Password);
            if (result == PasswordVerificationResult.Failed)
            {
                throw new NotFoundException("Hasło nie jest prawidłowe."); ;
            }

            if (result == PasswordVerificationResult.Success)
            {
                var getToken = GetToken(user);
                var refreshToken = GetRefreshToken(user);

                return new AuthResult()
                {
                    UserId = user.Id,
                    Token = getToken,
                    RefreshToken = refreshToken.Token,
                    ExpiresIn = _authenticationSettings.JwtExpireIn,
                    RefreshExpiresIn = _authenticationSettings.JwtRefreshExpiresIn,
                    TokenType = _authenticationSettings.token_type
                };
            }
            throw new BadRequestException("Something went wrong.");
        }

        private string GetToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_authenticationSettings.JwtKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.FirstName),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Role, user.Role),
                }),
                Expires = DateTime.UtcNow.AddSeconds(_authenticationSettings.JwtExpireIn),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);
            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }

        private RefreshToken GetRefreshToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, $"{user.Email}"),
                new Claim(ClaimTypes.Name, $"{user.FirstName} + {user.LastName}")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authenticationSettings.JwtKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var expires = DateTime.UtcNow.AddSeconds(_authenticationSettings.JwtRefreshExpiresIn);

            var token = new JwtSecurityToken(_authenticationSettings.JwtIssuer,
                _authenticationSettings.JwtIssuer,
                claims,
                expires: expires,
                signingCredentials: cred);

            var tokenHandler = new JwtSecurityTokenHandler();

            var refreshedToken = tokenHandler.WriteToken(token);
            var userId = _context.Users.Where(c => c.Email == user.Email).Select(c => c.Id).FirstOrDefault();

            var refreshToken = new RefreshToken()
            {
                UserId = userId,
                AddedDate = DateTime.UtcNow,
                ExpiryDate = expires,
                Token = refreshedToken
            };

            _context.SaveChangesAsync();

            return refreshToken;
        }
    }
}
