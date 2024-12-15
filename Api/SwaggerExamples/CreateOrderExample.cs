using Client.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace Api.SwaggerExamples;

// Great practice of integrating IExamplesProvider
public class CreateOrderExample : IExamplesProvider<CreateOrderRequestDto>
{
    // I know this part is an example of layout and format etc, but for readability I would rework the nested structure to make it clearer for an initial glance or read 
    // separating concerns makes it easier to reuse or modify specific parts 
    //public CreateOrderRequestDto GetExamples()
    //{
    //    var billingAddress = new AddressDto
    //    {
    //        AddressLineOne = "123 Billing Line One",
    //        AddressLineTwo = "Billing Line Two",
    //        AddressLineThree = "Billing Line Three",
    //        PostCode = "M1 1AB"
    //    };

    //    var shippingAddress = new AddressDto
    //    {
    //        AddressLineOne = "123 Shipping Line One",
    //        AddressLineTwo = "Shipping Line Two",
    //        AddressLineThree = "Shipping Line Three",
    //        PostCode = "M1 3CD"
    //    };

    //    var customer = new CustomerDto
    //    {
    //        CustomerName = "Customer Name",
    //        EmailAddress = "customername@email.com",
    //        PhoneNumber = "07123 456 789",
    //        BillingAddress = billingAddress,
    //        ShippingAddress = shippingAddress
    //    };

    //    var orderItems = new List<CreateOrderItemDto>
    //{
    //    new() { Quantity = 1, Sku = "BFRE011" },
    //    new() { Quantity = 2, Sku = "L1700FSLH" }
    //};

    //    return new CreateOrderRequestDto
    //    {
    //        Customer = customer,
    //        OrderItems = orderItems
    //    };
    //}

    // Data provided is well-structured, there could be edge cases I would be interested in knowing about for educational / testing purposes. e.g. empty order items to check 
    // validation of required fields, non-standard post codes, how do we handle UK vs republic of ireland etc, if orders are accepted there 
    // which fields are optional etc 
    public CreateOrderRequestDto GetExamples()
    {
        return new CreateOrderRequestDto
        {
            Customer = new CustomerDto
            {
                CustomerName = "Customer Name",
                EmailAddress = "customername@email.com",
                PhoneNumber = "07123 456 789",
                BillingAddress = new AddressDto
                {
                    AddressLineOne = "123 Billing Line One",
                    AddressLineTwo = "Billing Line Two",
                    AddressLineThree = "Billing Line Three",
                    PostCode = "M1 1AB"
                },
                ShippingAddress = new AddressDto
                {
                    AddressLineOne = "123 Shipping Line One",
                    AddressLineTwo = "Shipping Line Two",
                    AddressLineThree = "Shipping Line Three",
                    PostCode = "M1 3CD"
                }
            },
            OrderItems = new List<CreateOrderItemDto>
            {
                new ()
                {
                    Quantity = 1,
                    Sku = "BFRE011",
                },
                new()
                {
                    Quantity = 2,
                    Sku = "L1700FSLH",
                }
            },
        };
    }
}