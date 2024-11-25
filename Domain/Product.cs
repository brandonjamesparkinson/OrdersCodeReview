namespace Domain;

public class Product
{
    public int ProductId { get; private set; }
    public Guid ExternalId { get; private set; }
    public string Name { get; private set; }
    public string ImageUrl { get; private set; }

    public ICollection<Variant> Variants { get; private set; }
}