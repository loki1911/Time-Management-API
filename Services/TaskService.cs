using static System.Runtime.InteropServices.JavaScript.JSType;
using TimeMangementSystemAPI.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Data;
using Task = TimeMangementSystemAPI.Models.Task;

namespace TimeMangementSystemAPI.Services
{
    public class TaskService
    {
        private readonly string connectionstring;
         
        public TaskService(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("DefaultConnection");

        }


        public async Task<IEnumerable<Project>> GetProjectsAsync()
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                string query = "SELECT ProjectName, ProjectId FROM Project";
                var user = await db.QueryAsync<Project>(query);
                return (user);
            }
        }


        public async Task<Client> GetClientByProjectIdAsync(int projectId)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                string query = @"SELECT c.ClientId, c.ClientName
                         FROM Client c
                         JOIN Project p ON p.ClientId = c.ClientId
                         WHERE p.ProjectId = @ProjectId";

                return await db.QueryFirstOrDefaultAsync<Client>(query, new { ProjectId = projectId });
            }
        }


        public async Task<dynamic> SaveTaskDetailsAsync(Task taskDetails)
        {
            try
            {
                using (IDbConnection db = new SqlConnection(connectionstring))
                {
                    DynamicParameters dynamic = new DynamicParameters();
                    dynamic.Add("@ProjectId",taskDetails.ProjectId);
                    dynamic.Add("@ClientId", taskDetails.ClientId);
                    dynamic.Add("@BillingType", taskDetails.BillingType);
                    dynamic.Add("@TaskName", taskDetails.TaskName);
                    dynamic.Add("@Date", taskDetails.Date);
                    dynamic.Add("@TimeWorked", taskDetails.TimeWorked);
                    dynamic.Add("@Description", taskDetails.Description);
                    db.Query("dbo.inserttask",dynamic,null,true,20000,commandType:CommandType.StoredProcedure);
                }
                return null;

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<string>> GetTaskNameByProjectIdAsync(int ProjectId)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                string query = "SELECT TaskName From ProjectTask Where ProjectId = @ProjectId";
                return await db.QueryAsync<string>(query, new { ProjectId = ProjectId });
            }
        }


        public async Task<IEnumerable<Task>> GetTaskdetailsAsync()
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                string query = "SELECT * FROM TaskDetails";
                var user = await db.QueryAsync<Task>(query);
                return (user);
            }
        }
    }
}
