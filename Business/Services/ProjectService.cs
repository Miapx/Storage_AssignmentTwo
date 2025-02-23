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
            StartDate = form.StartDate,
            EndDate = form.EndDate,
            StatusName = form.StatusName,
            CustomerId = form.CustomerId,
        };


        var result = await _projectRepository.CreateAsync(project);
        return result;
    }

    public async Task<IEnumerable<Project>> GetAllProjectsAsync()
    {
        var project = await _projectRepository.GetAllAsync();
        return project.Select((ProjectEntity entity) => {
            return new Project(entity.Id, entity.Title, entity.Description, entity.StartDate, entity.EndDate, entity.StatusName, entity.Customer);
        });
        //Select((TAR IN ENTITY och via en FUNKTION mappar om till MODEL
    }

    public async Task<Project?> GetProjectById(int id)
    {
        var projectEntity = await _projectRepository.GetAsync(x => x.Id == id);
        if (projectEntity == null)
            return null;

        var projectModel = new Project
        {
            Id = projectEntity.Id,
            Title = projectEntity.Title,
            Description = projectEntity.Description,
            StartDate = projectEntity.StartDate,
            EndDate = projectEntity.EndDate,
            StatusName = projectEntity.StatusName,
            Customer = projectEntity.Customer
        };
        return projectModel;

        //annan approach: return new Project(projectEntity.Id, projectEntity.Title, projectEntity.Description, projectEntity.StartDate, projectEntity.EndDate, projectEntity.StatusName, projectEntity.Customer);
    }

    public async Task<Project?> UpdateProjectAsync(ProjectUpdateForm form)
    {
        var project = await _projectRepository.GetAsync(x => x.Id == form.Id);
        if (project == null)
            return null;

        project.Title = form.Title;
        project.Description = form.Description;
        project.StartDate = form.StartDate;
        project.EndDate = form.EndDate;
        project.StatusName = form.StatusName;
        project.CustomerId = form.CustomerId;


        await _projectRepository.UpdateAsync(project);
        project = await _projectRepository.GetAsync(x => x.Id == form.Id);
        //annan approach: return project != null ? new Project(project.Id, project.Title, project.Description, project.StartDate, project.EndDate, project.StatusName, project.Customer) : null;
        var projectModel = new Project
        {
            Id = form.Id,
            Title = project.Title,
            Description = project.Description,
            StartDate = project.StartDate,
            EndDate = project.EndDate,
            StatusName = project.StatusName,
            Customer = project.Customer
        };
        return projectModel; 
    }

    public async Task<bool> DeleteProjectAsync(int id)
    {
        var project = await _projectRepository.GetAsync(x => x.Id == id);
        if (project == null)
            return false;

        var result = await _projectRepository.DeleteAsync(project);
        return result;
    }
}
