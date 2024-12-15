namespace Client.Dtos;

public class CreateOrderItemDto
{
    public string Sku { get; set; } // non nullable property - suggest required 
    public int Quantity { get; set; }
}