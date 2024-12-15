using Client.Dtos;
using DataAccess;
using Domain;

namespace Application.Addresses;

// Class name is clear as to what is happening here
public class AddressProvider(IRepository<Address> addressRepo,
                             IUnitOfWork unitOfWork) : IAddressProvider
{
    // for readability I would expect constructor syntax to be like
    // more standard approach that others may find more readable 
    // 
    //public class AddressProvider : IAddressProvider
    //{
    //    private readonly IRepository<Address> _addressRepo;
    //    private readonly IUnitOfWork _unitOfWork;

    //    public AddressProvider(IRepository<Address> addressRepo, IUnitOfWork unitOfWork)
    //    {
    //        _addressRepo = addressRepo;
    //        _unitOfWork = unitOfWork;
    //    }

    private readonly IRepository<Address> _addressRepo = addressRepo;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    // Consider returning a smaller, self describing record with named properties e.g. Return new AddressPair(billingAddress, ShippingAddress) - clarity and readability 
    public Tuple<Address, Address> GetAddresses(AddressDto billingAddressDto,
                                                AddressDto shippingAddressDto)
    {
        // null forgiving use of the ! operator 
        // consider null checks and validation / exceptions on these 
        // expect that addressline2,3 are often not used in typical UK addresses anyway 
        var billingAddressHash = Address.GenerateAddressHash(billingAddressDto.AddressLineOne,
                                                             billingAddressDto.AddressLineTwo!,
                                                             billingAddressDto.AddressLineThree!,
                                                             billingAddressDto.PostCode);

        var shippingAddressHash = Address.GenerateAddressHash(shippingAddressDto.AddressLineOne,
                                                              shippingAddressDto.AddressLineTwo!,
                                                              shippingAddressDto.AddressLineThree!,
                                                              shippingAddressDto.PostCode);

        var lookupHashes = new Guid[] { billingAddressHash, shippingAddressHash };

        // great use of hashing - security considerations 
        var addresses = addressRepo.Get(x => lookupHashes.Contains(x.Hash));

        var billingAddress = addresses.SingleOrDefault(x => x.Hash == billingAddressHash)
                             ?? CreateAddress(billingAddressDto.AddressLineOne,
                                              billingAddressDto.AddressLineTwo!,
                                              billingAddressDto.AddressLineThree!,
                                              billingAddressDto.PostCode);

        var shippingAddress = addresses.SingleOrDefault(x => x.Hash == shippingAddressHash)
                              ?? CreateAddress(shippingAddressDto.AddressLineOne,
                                               shippingAddressDto.AddressLineTwo!,
                                               shippingAddressDto.AddressLineThree!,
                                               shippingAddressDto.PostCode);

        return new Tuple<Address, Address>(billingAddress, shippingAddress);
    }

    // encapsulation of address creation logic - here we create the address and save the changes at the same time 
    // Currently the method assumes the DTO and repository calls will succeed - may need to add exception handling here 
    private Address CreateAddress(string lineOne,
                                  string lineTwo,
                                  string lineThree,
                                  string postCode)
    {
        var address = new Address(lineOne,
                                  lineTwo,
                                  lineThree,
                                  postCode);

        _addressRepo.Insert(address);

        _unitOfWork.Save();

        return address;
    }
}