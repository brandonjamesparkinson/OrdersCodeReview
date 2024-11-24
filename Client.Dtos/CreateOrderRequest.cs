namespace Client.Dtos;

public class CreateOrderRequestDto
{
    public CustomerDto Customer { get; set; }

    public ICollection<CreateOrderItemDto> OrderItems { get; set; }
}

public class CustomerDto
{
    public string CustomerName { get; set; }
    public string EmailAddress { get; set; }
    public string? PhoneNumber { get; set; }
    public AddressDto BillingAddress { get; set; }
    public AddressDto ShippingAddress { get; set; }
}

public class AddressDto
{
    public string AddressLineOne { get; set; }
    public string? AddressLineTwo { get; set; }
    public string? AddressLineThree { get; set; }
    public string PostCode { get; set; }
}

public class CreateOrderItemDto
{
    public string Sku { get; set; }
    public int Quantity { get; set; }
}
