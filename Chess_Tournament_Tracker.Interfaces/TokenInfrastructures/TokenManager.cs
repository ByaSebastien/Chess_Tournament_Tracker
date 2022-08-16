using Chess_Tournament_Tracker.Models.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Chess_Tournament_Tracker.IL.TokenInfrastructures
{
    public class TokenManager
    {
        private readonly string _issuer, _audience, _secret;
        public TokenManager(IConfiguration config)
        {
            _issuer = config.GetSection("TokenInfo").GetSection("issuer").Value;
            _audience = config.GetSection("TokenInfo").GetSection("audience").Value;
            _secret = config.GetSection("TokenInfo").GetSection("secret").Value;
        }
        public string GenerateToken(User user)
        {
            if (user is null) throw new ArgumentNullException();

            //Creer la Signin key
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));

            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);

            //Creation du payload
            Claim[] claims = new Claim[]
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Role,user.IsAdmin ? "Admin" : "User"),
            };

            //Configuration du token
            JwtSecurityToken token = new JwtSecurityToken(
                    claims: claims,
                    signingCredentials: credentials,
                    issuer: _issuer,
                    audience: _audience
                );

            //Generation du token
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            return handler.WriteToken(token);
        }
    }
}
