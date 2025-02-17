using Business.Dtos;
using Business.Models;
using DataStorage.Entities;
using DataStorage.Repositories;

namespace Business.Services;

public class ProjectService(ProjectRepository projectRepository)
{
    private readonly ProjectRepository _projectRepository = projectRepository;

    public async Task<bool> CreateProjectAsync(ProjectRegistrationForm form)
    {
        var project = await _projectRepository.GetAsync(x => x.Title == form.Title);
        if (project != null)
            return false;

        project = new ProjectEntity
        {
            Title = form.Title,
            Description = form.Description,
            StartDate = DateTime.Now,
            EndDate = DateTime.MaxValue,
            CustomerId = form.CustomerId,
        };


        var result = await _projectRepository.CreateAsync(project);
        return result;
    }

    //READ
    public async Task<IEnumerable<Project>> GetAllCustomersAsync()
    {
        var project = await _projectRepository.GetAllAsync();
        return project.Select((ProjectEntity entity) => {
            return new Project(entity.Id, entity.Title, entity.Description, entity.StartDate, entity.EndDate, entity.Customer);
        });
        //Select((TAR IN ENTITY och via en FUNKTION mappar om till MODEL
    }

    public async Task<Project?> GetProjectById(int id)
    {
        var projectEntity = await _projectRepository.GetAsync(x => x.Id == id);
        if (projectEntity == null)
            return null;

        return new Project(projectEntity.Id, projectEntity.Title, projectEntity.Description, projectEntity.StartDate, projectEntity.EndDate, projectEntity.Customer);
    }

    //UPDATE (ProjectEntity ska skickas in i UpdateAsync.
    public async Task<Project?> UpdateProjectAsync(ProjectUpdateForm form)
    {
        var project = await _projectRepository.GetAsync(x => x.Id == form.Id);
        if (project == null)
            return null;

        project.Title = form.Title;
        project.Description = form.Description;
        project.StartDate = form.StartDate;
        project.EndDate = form.EndDate;
        project.Customer = form.Customer;


        await _projectRepository.UpdateAsync(project);
        project = await _projectRepository.GetAsync(x => x.Id == form.Id);
        return project != null ? new Project(project.Id, project.Title, project.Description, project.StartDate, project.EndDate, project.Customer) : null;
        //new Project(SKAPAR MODEL och via en KONSTRUKTOR mappar om till ENTITY 
    }

    //DELETE
    public async Task<bool> DeleteProjectAsync(int id)
    {
        var project = await _projectRepository.GetAsync(x => x.Id == id);
        if (project == null)
            return false;

        var result = await _projectRepository.DeleteAsync(project);
        return result;
    }
}
