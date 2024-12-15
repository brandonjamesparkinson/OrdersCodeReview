namespace Client.Dtos;

public class AddressDto
{
    public string AddressLineOne { get; set; } // non nullable property - suggest required 
    public string? AddressLineTwo { get; set; }
    public string? AddressLineThree { get; set; }
    public string PostCode { get; set; } // non nullable property - suggest required 
}