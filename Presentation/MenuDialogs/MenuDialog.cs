using Business.Dtos;
using Business.Services;

namespace Presentation.MenuDialogs;

public class MenuDialog(CustomerService customerService)
{
    private readonly CustomerService _customerService = customerService;

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
                //    await ProjectsDialog();
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
        customerUpdateForm.Id = Convert.ToInt32(chosenCustomer);

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

}
