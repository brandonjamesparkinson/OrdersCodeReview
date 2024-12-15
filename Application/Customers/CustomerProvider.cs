using Client.Dtos;
using DataAccess;
using Domain;

namespace Application.Customers;


public class CustomerProvider(IRepository<Customer> customerRepo,
                              IUnitOfWork unitOfWork) : ICustomerProvider
{
    private readonly IRepository<Customer> _customerRepo = customerRepo;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    // nothing wrong with current implemtation of constructor styling, however i would typically use a standard constructor with assigned fields inside - readable and consistent

    //public class CustomerProvider : ICustomerProvider
    //{
    //    private readonly IRepository<Customer> _customerRepo;
    //    private readonly IUnitOfWork _unitOfWork;

    //    public CustomerProvider(IRepository<Customer> customerRepo, IUnitOfWork unitOfWork)
    //    {
    //        _customerRepo = customerRepo;
    //        _unitOfWork = unitOfWork;
    //    }
    //}


    public Customer GetCustomer(CustomerDto request)
    {
        // if email address can be null, an exception could be thrown when calling .ToLowerInvariant
        if (string.IsNullOrWhiteSpace(request.EmailAddress))
        {
            throw new ArgumentException("Email address is required", nameof(request.EmailAddress));
        }
        // SingleOrDefault will throw an exception if more than one record with same email? would need to know if there is a unique constraint on db for this field 
        var customer = _customerRepo.Get(x => x.Email == request.EmailAddress.ToLowerInvariant())
                                    .SingleOrDefault();

        if (customer is not null)
            return customer;

        customer = new Customer(request.EmailAddress,
                                request.CustomerName,
                                request.PhoneNumber);

        customerRepo.Insert(customer);

        _unitOfWork.Save();

        return customer;
    }
}