using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using TimeMangementSystemAPI.Models;

namespace TimeMangementSystemAPI.Services
{
    public class ProjectService:projectInterface
    {
        private readonly string connectionstring;


        public ProjectService(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("DefaultConnection");

        }

       

        public async Task<Project> GetClientByProjectIdAsync(int projectId)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                string query = @"SELECT c.ClientId, c.ClientName
                         FROM Client c
                         JOIN Project p ON p.ClientId = c.ClientId
                         WHERE p.ProjectId = @ProjectId";

                return await db.QueryFirstOrDefaultAsync<Project>(query, new { ProjectId = projectId });
            }
        }

        public async Task<string> GetDepartmentNameByProjectIdAsync(int projectId)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                string query = @"
           SELECT d.DepartmentName
           FROM Department d
           JOIN Project p ON p.DepartmentID = d.DepartmentID
           WHERE p.ProjectID = @ProjectId";

                return await db.QueryFirstOrDefaultAsync<string>(query, new { ProjectId = projectId });
            }
        }



        public async Task<IEnumerable<Manager>> GetManagersByDepartmentAsync(int DepartmentID)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                var query = @"
            SELECT e.EmployeeID, e.EmployeeName
            FROM Employee e
            WHERE e.DepartmentID = @DepartmentID AND e.EmployeeDesignation = 'Manager'";

                return await db.QueryAsync<Manager>(query, new { DepartmentID = DepartmentID });
            }
        }


        public async Task<IEnumerable<GetData>> GetProjectData()
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                var query = "Select ProjectId, EmployeeName,ProjectDescription,p.ClientID,ProjectName,DepartmentName from Employee e inner join Project p on e.EmployeeID = p.ProjectManagerID inner join Department d on d.DepartmentID = p. DepartmentID  ";
                return await db.QueryAsync<GetData>(query);
            }
        }



        public int SaveEmployee(Projects projects)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                string query = @"Insert into Project(ProjectName ,ProjectDescription ,ClientId,DepartmentID,ProjectManagerID) values (@ProjectName ,@ProjectDescription,@ClientId,@DepartmentID,@ProjectManagerID)";
                return db.ExecuteScalar<int>(query, projects);
            }
        }

        public int UpdateProject(ProjectData projectData)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                string query = @"
                    UPDATE Project
                    SET  
                    ProjectName = @ProjectName,
                    ProjectDescription = @ProjectDescription,
                    ClientId = @ClientId,
                    DepartmentID = @DepartmentID,
                    ProjectManagerID = ProjectManagerID
                    WHERE ProjectId = ProjectId";

                return db.Execute(query, projectData);
            }
        }
    }


}

