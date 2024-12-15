using Client.Dtos;
using Domain;

namespace Application.Addresses;

public interface IAddressProvider
{
    // use of an interface is good, defins what is present in addressprovider 
    Tuple<Address, Address> GetAddresses(AddressDto billingAddressDto,
                                         AddressDto shippingAddressDto);
}