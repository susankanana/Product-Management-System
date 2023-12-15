using Microsoft.IdentityModel.Tokens;
using Product_Management_System.models;
using Product_Management_System.Services.IService;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Product_Management_System.Services
{
    public class JwtService : IJwt
    {
        private readonly IConfiguration _configuration;
        public JwtService(IConfiguration configuration)
        {

            _configuration = configuration;

        }
        public string GenerateToken(User user)
        {
            var secretKey = _configuration.GetSection("JwtOptions:SecretKey").Value;
            var audience = _configuration.GetSection("JwtOptions:Audience").Value;
            var issuer = _configuration.GetSection("JwtOptions:Issuer").Value;

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //payload
            List<Claim> claims = new List<Claim>() { };

            claims.Add(new Claim("Roles", user.Roles));
            claims.Add(new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString()));
            claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            claims.Add(new Claim(JwtRegisteredClaimNames.Name , user.Name));

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = issuer,
                Audience = audience,
                Expires = DateTime.UtcNow.AddHours(3),
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = cred

            };
            var token = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
