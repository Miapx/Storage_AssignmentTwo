using DataStorage.Entities;

namespace Business.Models;

public class Project
{
    public Project(int id, string title, string? description, DateTime startDate, DateTime endDate, string statusName, CustomerEntity customer)
    {
        Id = id;
        Title = title;
        Description = description;
        StartDate = startDate;
        EndDate = endDate;
        StatusName = statusName;
        Customer = customer;
    }

    public Project() { }

    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string StatusName { get; set; } = null!;

    //public int CustomerId { get; set; }
    public virtual CustomerEntity Customer { get; set; } = null!;
}
