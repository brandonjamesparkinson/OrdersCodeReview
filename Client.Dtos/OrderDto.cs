namespace Client.Dtos;

public class OrderDto
{
    // non nullable property - suggest required 
    public required string OrderNumber { get; set; }
    public string CustomerName { get; set; } // non nullable property - suggest required 
    public string? PhoneNumber { get; set; }
    public AddressDto BillingAddress { get; set; } // non nullable property - suggest required 
    public AddressDto ShippingAddress { get; set; } // non nullable property - suggest required 
    public decimal TotalCost { get; set; }

    public ICollection<OrderItemDto> OrderItems { get; set; } // non nullable property - suggest required 
}