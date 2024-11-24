using Domain;

namespace Application.Orders;

public interface ICreateOrderRequestValidator
{
    bool TryValidate(Customer customer,
                     Address billingAddress,
                     Address shippingAddress,
                     out IDictionary<string, string> errors);
}