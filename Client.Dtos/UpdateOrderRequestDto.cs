namespace Client.Dtos;

public class UpdateOrderRequestDto
{
    public string OrderNumber { get; set; } // non nullable property - suggest required 
    public AddressDto ShippingAddress { get; set; } // non nullable property - suggest required 
    public ICollection<CreateOrderItemDto> OrderItems { get; set; } // non nullable property - suggest required 
}