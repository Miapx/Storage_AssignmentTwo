using DataStorage.Entities;
using DataStorage.Repositories;

namespace Business.Services;

public class ProjectService(ProjectRepository projectRepository)
{
    private readonly ProjectRepository _projectRepository = projectRepository;

    //Ska skicka tillbaka modeller istället för entiteter, samt behöver kolla om någon existerar

    public async Task<IEnumerable<ProjectEntity>> GetAllProjectsAsync()
    {
        return await _projectRepository.GetAllAsync();
    }

    public async Task<ProjectEntity?> GetProjectById(int projectId)
    {
        return await _projectRepository.GetAsync(projectId);
    }
}
