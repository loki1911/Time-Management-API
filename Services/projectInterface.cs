using TimeMangementSystemAPI.Models;

namespace TimeMangementSystemAPI.Services
{
    public interface projectInterface
    {
        Task<Project> GetClientByProjectIdAsync(int projectId);
        //Task<IEnumerable<Project>> GetProjectsAsync();

        Task<IEnumerable<GetData>> GetProjectData();

        Task<string> GetDepartmentNameByProjectIdAsync(int projectId);

        Task<IEnumerable<Manager>> GetManagersByDepartmentAsync(int DepartmentID);

        
        int SaveEmployee(Projects projects);
        int UpdateProject(ProjectData projectData);
        
    }
}
