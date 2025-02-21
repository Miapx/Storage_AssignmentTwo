using System.ComponentModel.DataAnnotations.Schema;

namespace DataStorage.Entities;

public class ProjectEntity
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }

    [Column(TypeName = "date")]
    public DateTime StartDate { get; set; }

    [Column(TypeName = "date")]
    public DateTime EndDate { get; set; }
    public string StatusName { get; set; } = null!;

    public int CustomerId { get; set; }
    public virtual CustomerEntity Customer { get; set; } = null!;
}
