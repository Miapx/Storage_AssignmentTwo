using DataStorage.Contexts;
using DataStorage.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataStorage.Repositories;

public class ProjectRepository(DataContext context)
{
    private readonly DataContext _context = context;

    //CREATE

    public async Task<bool> CreateAsync(ProjectEntity entity)
    {
        try
        {
            _context.Projects.Add(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }


    //READ

    public async Task<IEnumerable<ProjectEntity>> GetAllAsync()
    {
        var entities = await _context.Projects.ToListAsync();
        return entities;
    }

    public async Task<ProjectEntity?> GetAsync(Expression<Func<ProjectEntity, bool>> expression)
    {
        return await _context.Projects.FirstOrDefaultAsync(expression);
    }

    //UPDATE

    public async Task<bool> UpdateAsync(ProjectEntity entity)
    {
        try
        
        {
            _context.Projects.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }


    //DELETE
    public async Task<bool> DeleteAsync(ProjectEntity entity)
    {
        try
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }
}
