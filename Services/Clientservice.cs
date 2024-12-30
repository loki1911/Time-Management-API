using System.Data;
using Dapper;
using Microsoft.Data.SqlClient;
using TimeMangementSystemAPI.Models;

namespace TimeMangementSystemAPI.Services
{
    public class Clientservice: IClient
    {
        private readonly string connectionstring;

        public Clientservice(IConfiguration configuration)
        {
            connectionstring = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task<IEnumerable<Client>> getClientdata()
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                var query = "select * from Client";
                return await db.QueryAsync<Client>(query);
            }
        }
        public async Task<IEnumerable<ManagerData>> getManagerData()
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                var query = "  select a.EmployeeName, b.ProjectManagerID from Employee a join Project b on  a.EmployeeId  = b.ProjectManagerId  where a.EmployeeDesignation = 'manager'";
                return await db.QueryAsync<ManagerData>(query);
            }
        }
        public async Task<IEnumerable<Department>> getDepartmentData()
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                var query = " select DepartmentId, DepartmentName  from Department; ";
                return await db.QueryAsync<Department>(query);
            }

        }


        public async Task<int> insertclient(Client client)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                var query = "INSERT INTO Client (ClientName) VALUES (@ClientName)";
                return await db.ExecuteScalarAsync<int>(query, client);
            }
        }

        public async Task<bool> editclient(Client client)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                var query = "update  Client set ClientName = @ClientName where ClientId = @ClientId ";
                var users = await db.ExecuteAsync(query, client);
                return users > 0;

            }
        }
        public async Task<IEnumerable<string>> GetBillingtypeByProjectIdAsync(int ProjectId)
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                string query = "SELECT BillingType From billingtypedetails Where ProjectId = @ProjectId";
                return await db.QueryAsync<string>(query, new { ProjectId = ProjectId });
            }
        }
        public async Task<IEnumerable<Role>> getRoleData()
        {
            using (IDbConnection db = new SqlConnection(connectionstring))
            {
                var query = "select RoleId, RoleName from Role;";
                return await db.QueryAsync<Role>(query);
            }

        }

    }
}
    

