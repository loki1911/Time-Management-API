using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using TimeMangementSystemAPI.Models;
using TimeMangementSystemAPI.Services;

namespace TimeMangementSystemAPI.Repository
{
    public class UserRepository : IUserDetails
    {
        private readonly string connectionstring;
        public UserRepository(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("DefaultConnection");

        }

        public List<UserDetailsDTO> GetEmployeeTasksByEmail(string employeeEmailId)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                string query = @"
                SELECT 
                    E.EmployeeEmailId, 
                    T.TaskId, 
                    P.ProjectName,
                    C.ClientName,
                    T.BillingType AS BillingName, 
                    T.Description AS TaskDescription,
                    T.TimeWorked,
                    T.Date
                FROM 
                    TimeSheets.dbo.Employee E
                JOIN 
                    TimeSheets.dbo.TaskDetails T
                    ON T.ClientID = E.ClientID
                LEFT JOIN 
                    TimeSheets.dbo.Client C
                    ON T.ClientID = C.ClientID
                LEFT JOIN 
                    TimeSheets.dbo.Project P
                    ON T.ProjectId = P.ProjectId
                WHERE 
                    E.EmployeeEmailId = @EmployeeEmailId
                ORDER BY 
                    T.Date DESC;
            ";

                return db.Query<UserDetailsDTO>(query, new { EmployeeEmailId = employeeEmailId }).AsList();
            }
        }
    }
}

