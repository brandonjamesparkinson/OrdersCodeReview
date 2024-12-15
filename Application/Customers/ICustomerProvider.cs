using Client.Dtos;
using Domain;

namespace Application.Customers;

public interface ICustomerProvider
{
    // use of interface, I always think of interfaces as 'contracts' e.g. what a 'CustomerProvider' should include .. in this case 'GetCustomer' request 
    Customer GetCustomer(CustomerDto request);
}