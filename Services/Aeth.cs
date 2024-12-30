using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TimeMangementSystemAPI.Models;

namespace TimeMangementSystemAPI.Services
{
    public class Aeth
    {
        private readonly string connectionstring;
        private readonly string _secretKey = "mM9e94Ibo5P8tk9ZiP2XqFS6r0JK1Z2Ra3Yr8Ff1p2g=";

        public Aeth(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("DefaultConnection");
        }
        public JwtResponse AuthenticateAsync(EmployeeLogin employeeData)
        {
            using (var connection = new SqlConnection(connectionstring))
            {
                var query = "SELECT    r.RoleName,   e.EmployeeEmailId,    e.EmployeeName,  e.Password FROM     [TimeSheets].[dbo].[Employee] e  JOIN     [TimeSheets].[dbo].[Role] r ON    e.RoleId = r.RoleId WHERE E.[EmployeeEmailId] = @EmployeeEmailId";
                var user = connection.QueryFirstOrDefault<EmployeeLogin>(query, new { EmployeeEmailId = employeeData.EmployeeEmailId });
                if (user == null)
                {
                    throw new Exception("No user was found");
                }


                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new[]
                {
         new Claim("Dcube", employeeData.EmployeeEmailId),
         new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
         new Claim(JwtRegisteredClaimNames.Iss, "JwtIssuer"),
         new Claim(JwtRegisteredClaimNames.Aud, "JwtAudience"),
     };

                var token = new JwtSecurityToken(
                    issuer: "JwtIssuer",
                    audience: "JwtAudience",
                    claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: credentials
                );

                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.WriteToken(token);

                return new JwtResponse()
                {
                    JwtToken = jwtToken,
                    RoleName = user.RoleName

                } ;
            }
        }
    }
}

