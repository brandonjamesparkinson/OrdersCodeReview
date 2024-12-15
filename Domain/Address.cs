using System.Security.Cryptography;
using System.Text;

namespace Domain;

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

    // immutable - property setters - good practice 
    public int AddressId { get; private set; }
    public Guid Hash { get; private init; }
    public string LineOne { get; private set; }
    public string LineTwo { get; private set; }
    public string LineThree { get; private set; }
    public string PostCode { get; private set; }
    public DateTime Created { get; private set; }

    public ICollection<Order> BillingOrders { get; private set; }
    public ICollection<Order> ShippingOrders { get; private set; }

    // Currently assumes that LineOne and PostCode are non-null and non-empty 
    // consider exception handling for these 
    //if (string.IsNullOrWhiteSpace(lineOne)) throw new ArgumentException("LineOne is required", nameof(lineOne));
    //if (string.IsNullOrWhiteSpace(postCode)) throw new ArgumentException("PostCode is required", nameof(postCode));

    public static Guid GenerateAddressHash(string lineOne,
                                           string lineTwo,
                                           string lineThree,
                                           string postCode)
    {
        var concatenatedAddress = $"{lineOne},{lineTwo},{lineThree},{postCode}";

        // MD5 considered weaker than other cryptograph 
        // consider a more secure hashing algorithm like SHA256 (although with googles' quantum chip breakthroughs this use is now questionable?)
        using var md5 = MD5.Create();

        var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(concatenatedAddress));

        return new Guid(bytes);
    }

    // good use if 'internal' suggesting it can only be used within the domain layer, good practice based on any defined 'domain rules'
    internal void Update(string lineOne,
                         string lineTwo,
                         string lineThree,
                         string postCode)
    {
        LineOne = lineOne;
        LineTwo = lineTwo;
        LineThree = lineThree;
        PostCode = postCode;
    }
}