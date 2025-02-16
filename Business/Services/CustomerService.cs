using Business.Dtos;
using Business.Models;
using DataStorage.Entities;
using DataStorage.Repositories;

namespace Business.Services;

public class CustomerService(CustomerRepository customerRepository)
{
    private readonly CustomerRepository _customerRepository = customerRepository;

    //CREATE
    public async Task<bool> CreateCustomerAsync(CustomerRegistrationForm form)
    {
        var customer = await _customerRepository.GetAsync(x => x.CustomerName == form.CustomerName);
        if (customer != null)
            return false;

            customer = new CustomerEntity
            {
                CustomerName = form.CustomerName,
            };
        

            var result = await _customerRepository.CreateAsync(customer);
            return result;
    }

    //READ
    public async Task<IEnumerable<Customer>> GetAllCustomersAsync()
    {
        var customers = await _customerRepository.GetAllAsync();
        return customers.Select((CustomerEntity entity) => { 
            return new Customer(entity.Id, entity.CustomerName); 
        });
        //Select((TAR IN ENTITY och via en FUNKTION mappar om till MODEL
    }

    public async Task<Customer?> GetCustomerById(int id)
    {
        var customerEntity = await _customerRepository.GetAsync(x => x.Id == id);
        if (customerEntity == null)
            return null;

        return new Customer(customerEntity.Id, customerEntity.CustomerName);
    }

    //UPDATE (CustomerEntity ska skickas in i UpdateAsync.
    public async Task<Customer?> UpdateCustomerAsync(CustomerUpdateForm form)
    {
        var customer = await _customerRepository.GetAsync(x => x.Id == form.Id);
        if (customer == null) 
            return null;

        customer.CustomerName = form.CustomerName;

        await _customerRepository.UpdateAsync(customer);
        customer = await _customerRepository.GetAsync(x => x.Id == form.Id);
        return customer != null ? new Customer(customer.Id, customer.CustomerName) : null;
        //new Customer(SKAPAR MODEL och via en KONSTRUKTOR mappar om till ENTITY 
    }

    //DELETE
    public async Task<bool> DeleteCustomerAsync(int id)
    {
        var customer = await _customerRepository.GetAsync(x =>x.Id == id);
        if(customer == null) 
            return false;

        var result = await _customerRepository.DeleteAsync(customer);
        return result;
    }
 
}
