using System.Security.Cryptography;
using System.Text;

namespace Domain;

public class Customer
{
    private Customer() {}

    public Customer(string  emailAddress,
                    string  name,
                    string? phoneNumber)
    {
        ExternalId = Guid.NewGuid();
        Email = emailAddress;
        Name = name;
        PhoneNumber = phoneNumber;
        Created = DateTime.UtcNow;
    }

    public int CustomerId { get; private init; }
    public Guid ExternalId { get; private init; }
    public string Email { get; private set; }
    public string Name { get; private set; }
    public string? PhoneNumber { get; private set; }
    public DateTime Created { get; private set; }
}

public class Address
{
    private Address() {}

    public Address(string lineOne,
                   string lineTwo,
                   string lineThree,
                   string postCode)
    {
        Hash = GenerateAddressHash(lineOne, lineTwo, lineThree, postCode);
        LineOne = lineOne;
        LineTwo = lineTwo;
        LineThree = lineThree;
        PostCode = postCode;
        Created = DateTime.UtcNow;
    }

    public int AddressId { get; private set; }
    public Guid Hash { get; private init; }
    public string LineOne { get; private set; }
    public string LineTwo { get; private set; }
    public string LineThree { get; private set; }
    public string PostCode { get; private set; }
    public DateTime Created { get; private set; }

    public static Guid GenerateAddressHash(string lineOne,
                                        string lineTwo,
                                        string lineThree,
                                        string postCode)
    {
        var concatenatedAddress = $"{lineOne},{lineTwo},{lineThree},{postCode}";

        using var md5 = MD5.Create();

        var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(concatenatedAddress));

        return new Guid(bytes);
    }
}