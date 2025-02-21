using Business.Dtos;
using Business.Models;
using Business.Services;

namespace Presentation.MenuDialogs;

public class MenuDialog(CustomerService customerService, ProjectService projectService)
{
    private readonly CustomerService _customerService = customerService;
    private readonly ProjectService _projectService = projectService;

    public async Task MainMenuDialogAsync()
    {
        Console.WriteLine("---------MAIN MENU---------");
        Console.WriteLine();
        Console.WriteLine("1. Manage customers");
        Console.WriteLine("2. Manage projects");
        var option = Console.ReadLine();

        switch (option) 
        {
            case "1": 
                await CustomerDialog();
                break;
            case "2":
                await ProjectsDialog();
                break;
        }
    }

    public async Task CustomerDialog()
    {
        Console.WriteLine("---------Manage customers---------");
        Console.WriteLine("1. Create new customer");
        Console.WriteLine("2. View All customers");
        Console.WriteLine("3. Update customer");
        Console.WriteLine("4. Delete customer");
        var option = Console.ReadLine();

        switch (option)
        {
            case "1":
                await CreateNewCustomerOption();
                break;
            case "2":
                await ViewAllCustomersOption();
                break;
            case "3":
                await UpdateCustomerOption();
                break;
            case "4":
                await DeleteCustomerOption();
                break;
        }
    }
    public async Task CreateNewCustomerOption()
    {
        var customerRegistrationForm = new CustomerRegistrationForm();

        Console.WriteLine("CREATE NEW CUSTOMER");
        Console.Write("Enter customer name: ");
        customerRegistrationForm.CustomerName = Console.ReadLine()!;

        var result = await _customerService.CreateCustomerAsync(customerRegistrationForm);
        if (result) 
        {
            Console.WriteLine("Customer was created sucessfully");
        }
        else
        {
            Console.WriteLine("Something went wrong when trying to create new customer");
        }
        Console.ReadKey();
    }

    public async Task ViewAllCustomersOption()
    {
        Console.WriteLine("VIEW ALL CUSTOMERS");

        var customers = await _customerService.GetAllCustomersAsync();

        if (customers != null) 
        {
            foreach (var customer in customers)
                Console.WriteLine($"Id: {customer.Id}. Name: {customer.CustomerName}");
        }
        else
        {
            Console.WriteLine("No customers found");
        }
        Console.ReadKey();
    }


    public async Task UpdateCustomerOption()
    {
        Console.WriteLine("UPDATE CUSTOMER");
        Console.WriteLine("These are all the customers: ");


        var customers = await _customerService.GetAllCustomersAsync();

        if (customers != null)
        {
            foreach (var customer in customers)
                Console.WriteLine($"Id: {customer.Id}. Name: {customer.CustomerName}");
        }
        else
        {
            Console.WriteLine("No customers found");
        }

        var customerUpdateForm = new CustomerUpdateForm();
        Console.Write("Enter the ID of the Customer you want to update: ");
        var chosenCustomer = Console.ReadLine();
        try { 
        customerUpdateForm.Id = Convert.ToInt32(chosenCustomer);
            }
        catch
        {
            Console.WriteLine("Invalid ID. Press any key to return to main menu.");
            Console.ReadKey();
            return;
        }

        Console.WriteLine("Enter the name of you wish to update to");
        customerUpdateForm.CustomerName = Console.ReadLine()!;
        var result = await _customerService.UpdateCustomerAsync(customerUpdateForm);

        if (result != null)
        {
            Console.WriteLine("Customer was updated succesfully");
        }
        else
        {
            Console.WriteLine("Something went wrong when trying to update customer");
        }
        Console.ReadKey();
    }

    public async Task DeleteCustomerOption()
    {
        Console.WriteLine("DELETE CUSTOMER");
        Console.WriteLine("These are all the customers: ");


        var customers = await _customerService.GetAllCustomersAsync();

        if (customers != null)
        {
            foreach (var customer in customers)
                Console.WriteLine($"Id: {customer.Id}. Name: {customer.CustomerName}");
        }
        else
        {
            Console.WriteLine("No customers found");
        }

        Console.Write("Enter the ID of the Customer you want to delete: ");
        var customerDeleteOption = Console.ReadLine();
        var customerDeleteOptionId = Convert.ToInt32(customerDeleteOption);
        var result = await _customerService.DeleteCustomerAsync(customerDeleteOptionId);
        if (result) 
        {
            Console.WriteLine("Customer was deleted succesfully");
        }
        else
        {
            Console.WriteLine("Something went wrong when trying to delete customer");
        }
        Console.ReadKey();
    }


    public async Task ProjectsDialog()
    {
        Console.WriteLine("---------Manage projects---------");
        Console.WriteLine("1. Create new project");
        Console.WriteLine("2. View All projects");
        Console.WriteLine("3. Update project");
        Console.WriteLine("4. Delete project");
        var option = Console.ReadLine();

        switch (option)
        {
            case "1":
                await CreateNewProjectOption();
                break;
            case "2":
                await ViewAllProjectsOption();
                break;
            case "3":
                await UpdateProjectOption();
                break;
            case "4":
                await DeleteProjectOption();
                break;
        }
    }

    public async Task CreateNewProjectOption()
    {
        var projectRegistrationForm = new ProjectRegistrationForm();

        Console.WriteLine("CREATE NEW PROJECT");
        Console.Write("Enter projects title: ");
        projectRegistrationForm.Title = Console.ReadLine()!;
        Console.Write("Enter projects description: ");
        projectRegistrationForm.Description = Console.ReadLine()!;
        Console.Write("Enter projects start date (DD/MM/YY): ");
        var projectStartDate = Console.ReadLine()!;
        projectRegistrationForm.StartDate = Convert.ToDateTime(projectStartDate);

        Console.Write("Enter projects end date (DD/MM/YY): ");
        var projectEndDate = Console.ReadLine()!;
        projectRegistrationForm.EndDate = Convert.ToDateTime(projectEndDate);

        Console.WriteLine("Please type in if the project is 'Not started', 'In progress' or 'Finished'");
        projectRegistrationForm.StatusName = Console.ReadLine()!;

        var allCustomers = await _customerService.GetAllCustomersAsync();
        foreach (var customer in allCustomers)
        {
            Console.WriteLine($"ID: {customer.Id} Name: {customer.CustomerName}");
        }

        Console.Write("Above is a list of all our customers. " +
            "Enter the ID of the customer assigned to this project: ");
        var projectsCustomer = Console.ReadLine()!;
        projectRegistrationForm.CustomerId = Convert.ToInt32(projectsCustomer);

        var result = await _projectService.CreateProjectAsync(projectRegistrationForm);
        if (result)
        {
            Console.WriteLine("Project was created sucessfully");
        }
        else
        {
            Console.WriteLine("Something went wrong when trying to create new project");
        }
        Console.ReadKey();
    }

    public async Task ViewAllProjectsOption()
    {
        Console.WriteLine("VIEW ALL PROJECTS");

        var projects = await _projectService.GetAllProjectsAsync();

        if (projects != null)
        {
            foreach (var project in projects)
                Console.WriteLine($"ID: {project.Id}, Title: {project.Title}, Description: {project.Description}, Start-date: {project.StartDate}, End-date: {project.EndDate}, Status: {project.StatusName}, Customer: {project.Customer.CustomerName}");
        }
        else
        {
            Console.WriteLine("No projects found");
        }
        Console.ReadKey();
    }

    public async Task UpdateProjectOption()
    {
        Console.WriteLine("UPDATE PROJECT");
        Console.WriteLine("These are all the projects: ");

        var projects = await _projectService.GetAllProjectsAsync();

        if (projects != null)
        {
            foreach (var project in projects)
                Console.WriteLine($"Id: {project.Id}. Name: {project.Title}");
        }
        else
        {
            Console.WriteLine("No projects found");
        }

        var projectUpdateForm = new ProjectUpdateForm();
        Console.Write("Enter the ID of the Project you want to update: ");
        var chosenProject = Console.ReadLine();
        try {  
        projectUpdateForm.Id = Convert.ToInt32(chosenProject);
            }
        catch
        {
            Console.WriteLine("Invalid ID. Press any key to return to main menu");
            Console.ReadKey();
            return;
        }
        Console.Write("Enter the name of you wish to update to: ");
        projectUpdateForm.Title = Console.ReadLine()!;

        Console.Write("Enter the description: ");
        projectUpdateForm.Description = Console.ReadLine()!;

        Console.Write("Enter projects start date (DD/MM/YY): ");
        var projectStartDate = Console.ReadLine()!;
        projectUpdateForm.StartDate = Convert.ToDateTime(projectStartDate);

        Console.Write("Enter projects end date (DD/MM/YY): ");
        var projectEndDate = Console.ReadLine()!;
        projectUpdateForm.EndDate = Convert.ToDateTime(projectEndDate);

        Console.Write("Type in if the project is 'Not started', 'In progress' or 'Finished': ");
        projectUpdateForm.StatusName = Console.ReadLine()!;

        Console.WriteLine("These are all the customers: ");


        var customers = await _customerService.GetAllCustomersAsync();

        if (customers != null)
        {
            foreach (var customer in customers)
                Console.WriteLine($"Id: {customer.Id}. Name: {customer.CustomerName}");
        }
        else
        {
            Console.WriteLine("No customers found");
        }

        Console.Write("Enter the ID of the customer on this project: ");
        var updatedProjectCustomer = Console.ReadLine()!;
        projectUpdateForm.CustomerId = Convert.ToInt32(updatedProjectCustomer);

        Console.WriteLine("Press Y to save your changes, or N to cancel. ");
        var option = Console.ReadLine()!;

        if (option.ToUpper() == "Y")
        {

            var result = await _projectService.UpdateProjectAsync(projectUpdateForm);

            if (result != null)
            {
                Console.WriteLine("Project was updated succesfully");
            }
            else
            {
                Console.WriteLine("Something went wrong when trying to update project");
            }
            Console.ReadKey();
        }   
        else return;
    }
 

    public async Task DeleteProjectOption()
    {
        Console.WriteLine("DELETE PROJECT");
        Console.WriteLine("These are all the projects: ");


        var projects = await _projectService.GetAllProjectsAsync();

        if (projects != null)
        {
            foreach (var project in projects)
                Console.WriteLine($"Id: {project.Id}. Name: {project.Title}");
        }
        else
        {
            Console.WriteLine("No project found");
        }

        Console.Write("Enter the ID of the project you want to delete: ");
        var projectDeleteOption = Console.ReadLine();
        try
        {
            var projectDeleteOptionId = Convert.ToInt32(projectDeleteOption);
            var result = await _projectService.DeleteProjectAsync(projectDeleteOptionId);
            if (result)
            {
                Console.WriteLine("Project was deleted succesfully");
            }
            else
            {
                Console.WriteLine("Something went wrong when trying to delete project");
            }
        }
        catch
        {
            Console.WriteLine("No project with that ID was found. Press any key to return to main menu. ");
            Console.ReadKey();
            return;
        }

        Console.ReadKey();
    }
}
