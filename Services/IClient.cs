using TimeMangementSystemAPI.Models;

namespace TimeMangementSystemAPI.Services
{
    public interface IClient
    {
        Task<IEnumerable<Client>> getClientdata();
        Task<int> insertclient(Client client);
        Task<bool> editclient(Client Client);

        Task<IEnumerable<string>> GetBillingtypeByProjectIdAsync(int ProjectId);
         Task<IEnumerable<ManagerData>> getManagerData();
        Task<IEnumerable<Department>> getDepartmentData();

        Task<IEnumerable<Role>> getRoleData();

    }
}
