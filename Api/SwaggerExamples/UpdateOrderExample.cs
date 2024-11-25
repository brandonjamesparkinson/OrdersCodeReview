using Client.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace Api.SwaggerExamples;

public class UpdateOrderExample : IExamplesProvider<UpdateOrderRequestDto>
{
    public UpdateOrderRequestDto GetExamples()
    {
        return new UpdateOrderRequestDto
        {
            OrderNumber = "<GET FROM CREATE>",
            ShippingAddress = new AddressDto
            {
                AddressLineOne = "123 Updated Shipping Line One",
                AddressLineTwo = "Updated Shipping Line Two",
                AddressLineThree = "Updated Shipping Line Three",
                PostCode = "M1 3DE"
            },
            OrderItems = new List<CreateOrderItemDto>
            {
                new ()
                {
                    Quantity = 1,
                    Sku = "SKU-ONE",
                },
                new() 
                {
                    Quantity = 2,
                    Sku = "SKU-TWO",
                },
                new()
                {
                    Quantity = 3,
                    Sku = "SKU-THREE",
                }
            },
        };
    }
}