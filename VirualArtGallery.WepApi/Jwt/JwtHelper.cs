using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace VirualArtGallery.WepApi.Jwt
{
    public static class JwtHelper
    {
        public static string GenerateJwtToken(JwtDto jwt)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SecretKey));

            //Credentials->Kimlik bilgileri için 
            var credentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);


            var claims = new[]
            {
                 new Claim (JwtClaimNames.Id,jwt.Id.ToString()),
                 new Claim (JwtClaimNames.Id,jwt.Email),
                 new Claim (JwtClaimNames.Id,jwt.FirstName),
                 new Claim (JwtClaimNames.Id,jwt.LastName),
                 new Claim (JwtClaimNames.Id,jwt.UserType.ToString()),


                 new Claim (ClaimTypes.Role,jwt.UserType.ToString())

             };
            var expireTime = DateTime.Now.AddMinutes(jwt.ExpireMinutes);

            var tokenDescriptor = new JwtSecurityToken(jwt.Issuer, jwt.Audience, claims, null, expireTime, credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            return token;
        }
    }
}
