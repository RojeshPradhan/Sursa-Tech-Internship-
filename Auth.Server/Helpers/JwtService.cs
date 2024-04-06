using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Auth.Server.Helpers
{
    public class JwtService
    {
       private string secureKey = "this is a very secure key ekdam danger security vako key ho sushreeta le vaneko";

        public string Generate(int id)
        {
            var symmertricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secureKey));
            var credentials = new SigningCredentials(symmertricSecurityKey, algorithm: SecurityAlgorithms.HmacSha256Signature);
            var header = new JwtHeader(credentials);

            var payload = new JwtPayload(issuer:id.ToString(), audience: null,claims:null, notBefore:null, expires:DateTime.Today.AddDays(1));
            var securityToken = new JwtSecurityToken(header, payload); 

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
            }
         public JwtSecurityToken Verify(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(secureKey);
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validatedToken);
            return (JwtSecurityToken)validatedToken;
            }
        } 
    }

