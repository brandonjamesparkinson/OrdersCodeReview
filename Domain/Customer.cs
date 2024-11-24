namespace Domain;

public class Customer
{
    public int CustomerId { get; private init; }
    public Guid ExternalId { get; private init; }
    public string Name { get; private set; }
    public string Email { get; private set; }
    public string? PhoneNumber { get; private set; }
}

public class Address
{
    public int AddressId { get; private set; }
    public string LineOne { get; private set; }
    public string LineTwo { get; private set; }
    public string LineThree { get; private set; }
    public string PostCode { get; private set; }
}