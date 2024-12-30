using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace TimeMangementSystemAPI.Middlewares
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _secretKey;
        private readonly string _issuer;
        private readonly string _audience;

        public JWTMiddleware(RequestDelegate next, string secretKey, string issuer, string audience)
        {
            _next = next;
            _secretKey = secretKey;
            _issuer = issuer;
            _audience = audience;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            if (context.Request.Headers.TryGetValue("Authorization", out var token))
            {
                if (token.ToString().StartsWith("Bearer "))
                {
                    var jwtToken = token.ToString().Substring("Bearer ".Length).Trim();
                    ValidateToken(context, jwtToken);
                }
            }

            await _next(context);
        }

        private void ValidateToken(HttpContext context, string jwtToken)
        {
            var key = Encoding.UTF8.GetBytes("mM9e94Ibo5P8tk9ZiP2XqFS6r0JK1Z2Ra3Yr8Ff1p2g="); // Use the secret key used during token generation


            var tokenHandler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = "JwtIssuer",
                ValidateAudience = true,
                ValidAudiences = new[] { "JwtAudience" },
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };


            try
            {
                var principal = tokenHandler.ValidateToken(jwtToken, validationParameters, out var validatedToken);
                Console.WriteLine("Token is valid.");
            }
            catch (SecurityTokenExpiredException)
            {
                Console.WriteLine("Token has expired.");
            }
            catch (SecurityTokenException)
            {
                Console.WriteLine("Token is invalid.");
            }
        }
    }
}
