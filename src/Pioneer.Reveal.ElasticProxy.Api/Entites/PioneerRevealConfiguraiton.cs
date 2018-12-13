using System;
using Microsoft.IdentityModel.Tokens;

namespace Pioneer.Reveal.ElasticProxy.Api.Entites
{
    public class PioneerRevealConfiguraiton
    {
        public string JwtSecret { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string SiteUrl { get; set; }
        public SymmetricSecurityKey SymmetricSecurityKey => new SymmetricSecurityKey(Convert.FromBase64String(JwtSecret));
        public SigningCredentials SigningCredentials => new SigningCredentials(SymmetricSecurityKey, SecurityAlgorithms.HmacSha256);
    }
}
