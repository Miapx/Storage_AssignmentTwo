using Business.Services;
using DataStorage.Contexts;
using DataStorage.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Presentation.MenuDialogs;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Design;

JsonSerializerOptions options = new()
{
    WriteIndented = true,
    ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
};

var services = new ServiceCollection()
    .AddDbContext<DataContext>(x => x.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Projects\\Storage_AssignmentTwo\\DataStorage\\Databases\\local_db.mdf;Integrated Security=True;Connect Timeout=30"))
    .AddScoped<ProjectRepository>()
    .AddScoped<ProjectService>()
    .AddScoped<CustomerRepository>()
    .AddScoped<CustomerService>()
    .AddScoped<MenuDialog>()
    .BuildServiceProvider();

var menuDialog = services.GetRequiredService<MenuDialog>();
await menuDialog.MainMenuDialogAsync();

while (true)
{
    await menuDialog.MainMenuDialogAsync();
}

