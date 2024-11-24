using System.Security.Cryptography;
using System.Text;

namespace Domain;

public class Order
{
    private Order()
    {
        OrderItems = new HashSet<OrderItem>();
    }

    public Order(Customer customer,
                 Address shippingAddress,
                 Address billingAddress)
    {
        CustomerId = customer.CustomerId;
        Customer = customer;

        ShippingAddressId = shippingAddress.AddressId;
        ShippingAddress = shippingAddress;

        BillingAddressId = billingAddress.AddressId;
        BillingAddress = billingAddress;

        Created = DateTime.Now;
        LastModified = DateTime.Now;

        OrderNumber = GenerateOrderNumber();
    }

    public int OrderId { get; private set; }
    public string OrderNumber { get; private set; }

    public DateTime Created { get; private set; }
    public DateTime LastModified { get; private set; }

    public decimal TotalPrice { get; private set; }

    public int CustomerId { get; private set; }
    public int BillingAddressId { get; private set; }
    public int ShippingAddressId { get; private set; }

    public Customer Customer { get; private set; }
    public Address BillingAddress { get; private set; }
    public Address ShippingAddress { get; private set; }
    public ICollection<OrderItem> OrderItems { get; private set; }

    private string GenerateOrderNumber()
    {
        var seed =
            $"{Customer.Email}|{BillingAddress.LineOne}{BillingAddress.PostCode}|{ShippingAddress.LineOne}{ShippingAddress.LineTwo}|{Created}";

        using var md5 = MD5.Create();

        var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(seed));

        return $"ORD-{Created.Year % 1000}{Created.Month}-{bytes.GetHashCode()}";
    }

    public void UpdateItems(IDictionary<Variant, int> orderItems)
    {
        var orderedItems = orderItems.Select(x => new OrderItem(this,
                                                                x.Key,
                                                                x.Value))
                                     .ToList();

        OrderItems.Clear();

        foreach(var item in orderedItems)
            OrderItems.Add(item);
    }
}

public class OrderItem
{
    private OrderItem() {}

    public OrderItem(Order order,
                     Variant variant,
                     int quantity)
    {
        OrderId = order.OrderId;
        Order = order;

        VariantId = variant.VariantId;
        Variant = variant;

        Quantity = quantity;
    }

    public int OrderItemId { get; private set; }
    public int OrderId { get; private set; }
    public int VariantId { get; private set; }
    public int Quantity { get; set; }

    public Order Order { get; set; }
    public Variant Variant { get; set; }
}

public class Product
{
    public int ProductId { get; private set; }
    public Guid ExternalId { get; private set; }
    public string Name { get; private set; }
    public string ImageUrl { get; private set; }
}

public class Variant
{
    public int VariantId { get; private set; }
    public Guid ExternalId { get; private set; }
    public string Name { get; private set; }
    public string Sku { get; private set; }
    public decimal Price { get; private set; }
}