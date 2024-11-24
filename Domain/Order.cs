namespace Domain;

public class Order
{
    public int OrderId { get; private set; }
    public string OrderNumber { get; private set; }

    public long Created { get; private set; }
    public long LastModified { get; private set; }

    public decimal TotalPrice { get; private set; }

    public int BillingAddressId { get; private set; }
    public int ShippingAddressId { get; private set; }

    public Address BillingAddress { get; private set; }
    public Address ShippingAddress { get; private set; }
}

public class OrderItem
{
    public int OrderItemId { get; private set; }
    public int OrderId { get; private set; }
    public int VariantId { get; private set; }
    public int Quantity { get; set; }
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