using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using BetterCommerce.Core.Extensions;
using BetterCommerce.Core.Identity;
using BetterCommerce.Core.Security.Encryption;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace BetterCommerce.Core.Security.Jwt
{
    public class JwtHelper : ITokenHelper
    {
        private readonly DateTime _accessTokenExpiration;

        public JwtHelper(IConfiguration configuration)
        {
            _accessTokenExpiration = DateTime.Now.AddMinutes(TokenOptions.AccessTokenExpiration);
        }


        public AccessToken CreateToken(ApplicationUser user, List<ApplicationRole> roles)
        {
            var securityKey = SecurityKeyHelper.CreateSecurityKey(TokenOptions.SecurityKey);
            var signingCredentials = SigningCredentialsHelper.CreateSigningCredentials(securityKey);
            var jwt = CreateJwtSecurityToken(user, signingCredentials, roles);
            var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
            var token = jwtSecurityTokenHandler.WriteToken(jwt);
            
            return new AccessToken
            {
                Token = token,
                Expiration = _accessTokenExpiration
            };
        }

        public JwtSecurityToken CreateJwtSecurityToken(ApplicationUser user, SigningCredentials signingCredentials, List<ApplicationRole> applicationRoles)
        {
            var jwt = new JwtSecurityToken(
                issuer: TokenOptions.Issuer,
                audience: TokenOptions.Audience,
                expires: _accessTokenExpiration,
                notBefore: DateTime.Now,
                claims: SetClaims(user, applicationRoles),
                signingCredentials: signingCredentials
            );

            return jwt;
        }

        private IEnumerable<Claim> SetClaims(ApplicationUser user, List<ApplicationRole> applicationRoles)
        {
            var claims = new List<Claim>();
            claims.AddNameIdentifier(user.Id);
            claims.AddEmail(user.Email);
            claims.AddName($"{user.FirstName} {user.LastName}");
            claims.AddRoles(applicationRoles.Select(c => c.Name).ToArray());
            return claims;
        }
    }
}