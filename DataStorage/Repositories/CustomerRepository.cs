using DataStorage.Contexts;
using DataStorage.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataStorage.Repositories;

public class CustomerRepository(DataContext context)
{
    private readonly DataContext _context = context;

    //CREATE

    public async Task<bool> CreateAsync(CustomerEntity entity)
    {
        try
        {
            _context.Customers.Add(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }


    //READ

    public async Task<IEnumerable<CustomerEntity>> GetAllAsync()
    {
        var entities = await _context.Customers.ToListAsync();
        return entities;
    }

    public async Task<CustomerEntity?> GetAsync(Expression<Func<CustomerEntity, bool>> expression)
    {
        return await _context.Customers.FirstOrDefaultAsync(expression);
    }

    //UPDATE

    public async Task<bool> UpdateAsync(CustomerEntity entity)
    {
        try
        {
            _context.Customers.Update(entity);
            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }


    //DELETE
    public async Task<bool> DeleteAsync(CustomerEntity entity)
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


        //    var entity = await _context.Customers.FirstOrDefaultAsync(x => x.Id == id);
        //    if (entity != null)
        //    {
        //        _context.Customers.Remove(entity);
        //        await _context.SaveChangesAsync();
        //        return true;
        //    }
        //    return false;
        //}

    } 
}
