using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace B2B
{
    public class Authentication
    {
        public static bool IsAuthenticated(HttpRequest Response)
        {
            string token = Cookie.Get(Response, "authtoken");
            string username = Cookie.Get(Response, "username");
            return !string.IsNullOrEmpty(token) && ValidateToken(username, token);
        }

        public static bool ValidateToken(string username, string token)
        {

            if (token == null)
                return false;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("XXXYYYZZZThisIsTheKeyyyyyy");
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                if (jwtToken.ValidTo > DateTime.Now)
                    return true;


                // return user id from JWT token if validation successful
                return true;
            }
            catch
            {
                // return null if validation fails
                return false;
            }
        }
    }
}
