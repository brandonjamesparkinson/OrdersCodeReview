namespace Domain;

public class Variant
{
    public int VariantId { get; private set; }
    public Guid ExternalId { get; private set; }
    public string Name { get; private set; }
    public string Sku { get; private set; }
    public decimal Price { get; private set; }

    public Product Product { get; private set; }
}