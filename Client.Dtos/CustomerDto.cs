namespace Client.Dtos;

public class CustomerDto
{
    public string CustomerName { get; set; } // non nullable property - suggest required 
    public string EmailAddress { get; set; } // non nullable property - suggest required 
    public string? PhoneNumber { get; set; }
    public AddressDto BillingAddress { get; set; } // non nullable property - suggest required 
    public AddressDto ShippingAddress { get; set; } // non nullable property - suggest required 
}