namespace Client.Dtos;

public class CreateOrderRequestDto
{
    public CustomerDto Customer { get; set; } // non nullable property - suggest required 

    public ICollection<CreateOrderItemDto> OrderItems { get; set; } // non nullable property - suggest required 
}