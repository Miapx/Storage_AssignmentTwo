﻿using DataStorage.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business.Dtos;

public class ProjectUpdateForm
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string StatusName { get; set; } = null!;
    public int CustomerId { get; set; }
    public virtual CustomerEntity Customer { get; set; } = null!;
}
