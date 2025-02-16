namespace Business.Models;

public class Customer
{
    public Customer(int id, string customerName)
    {
        Id = id;
        CustomerName = customerName;
    }

    public Customer() 
    { 

    }

    public int Id { get; set; }
    public string CustomerName { get; set; } = null!;
}
