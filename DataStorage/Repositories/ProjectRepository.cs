using DataStorage.Contexts;
using DataStorage.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataStorage.Repositories;

public class ProjectRepository(DataContext context)
{
    private readonly DataContext _context = context;

    //READ
    public async Task<IEnumerable<ProjectEntity>> GetAllAsync()
    {
        return await _context.Projects.ToListAsync();
    }

    public async Task<ProjectEntity?> GetAsync(int id)
    {
        return await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);
    }

    //DELETE
    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);
        if (entity != null)
        {
            _context.Projects.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
