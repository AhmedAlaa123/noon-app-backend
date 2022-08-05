using System.IdentityModel.Tokens.Jwt;

namespace noone.Helpers
{
    public class TokenConverter
    {
        public static JwtSecurityToken ConvertToken(string Token)
        {
            JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();
            if (!securityTokenHandler.CanReadToken(Token))
                return null;
            return securityTokenHandler.ReadJwtToken(Token);
        }
    }
}
