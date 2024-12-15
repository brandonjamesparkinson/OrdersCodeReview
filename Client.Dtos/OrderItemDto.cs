namespace Client.Dtos;

public class OrderItemDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; } // non nullable property - suggest required 
    public int VariantId { get; set; }
    public string VariantName { get; set; } // non nullable property - suggest required 
    public string Sku { get; set; } // non nullable property - suggest required 
    public string ImageUrl { get; set; } // non nullable property - suggest required 
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal TotalPrice { get; set; }
}