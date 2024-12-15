using Client.Dtos;
using Swashbuckle.AspNetCore.Filters;

namespace Api.SwaggerExamples;

public class UpdateOrderExample : IExamplesProvider<UpdateOrderRequestDto>
{

    // Currently OrderItems only demonstrates 'Create' or 'Update' scenarios, would update allow removing 
    public UpdateOrderRequestDto GetExamples()
    {
        return new UpdateOrderRequestDto
        {
            OrderNumber = "<GET FROM CREATE>", // paramter indicates that the value is dependent on a prior operation - suggest a generic order number e.g. ORD123 

            // nested shipping address COULD make it harder to mdify in future, could be extracted into a helper variable for clarity and future re-use, e.g.

            //var updatedShippingAddress = new AddressDto
            //{
            //    AddressLineOne = "123 Updated Shipping Line One",
            //    AddressLineTwo = "Updated Shipping Line Two",
            //    AddressLineThree = "Updated Shipping Line Three",
            //    PostCode = "M1 3DE"
            //};
            //ShippingAddress = updatedShippingAddress

            ShippingAddress = new AddressDto
            {
                AddressLineOne = "123 Updated Shipping Line One",
                AddressLineTwo = "Updated Shipping Line Two",
                AddressLineThree = "Updated Shipping Line Three",
                PostCode = "M1 3DE"
            },
            OrderItems = new List<CreateOrderItemDto>
            {
                // Generic use of constructors, suggest NewCreateOrderItemDto within the List - for readability
                //new CreateOrderItemDto()
                //{
                    //Quantity = 1,
                    //Sku = "BFRE011",
                //}

                new () 
                {
                    Quantity = 1,
                    Sku = "BFRE011",
                },
                new()
                {
                    Quantity = 1,
                    Sku = "L1700FSLH",
                },
                new()
                {
                    Quantity = 1,
                    Sku = "CRZ-PK",
                }
            },
        };
    }
}