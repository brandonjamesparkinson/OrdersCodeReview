using System.Text.RegularExpressions;
using Domain;

namespace Application.Orders.Validators;

public class CreateOrderRequestValidator : ICreateOrderRequestValidator
{
    // Well structured and readable - assume it is stacked vertically in terms of what is filled out on the form 
    // e.g. Name, email, addresses 

    // regex - may be worth storing static regex for performance gains
    //private static readonly Regex EmailRegex = new(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    //private static readonly Regex PostcodeRegex = new(@"^(GIR 0AA|...)$", RegexOptions.IgnoreCase | RegexOptions.Compiled);
    public bool TryValidate(Customer customer,
                            Address billingAddress,
                            Address shippingAddress,
                            out IDictionary<string, string> errors)
    {
        errors = new Dictionary<string, string>();

        if (string.IsNullOrWhiteSpace(customer.Name))
            errors.Add(nameof(customer.Name), "Name is required");

        if (!Regex.IsMatch(customer.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase))
            errors.Add(nameof(customer.Email), "Email is not valid");

        if (customer.Created > DateTime.Now)
            errors.Add(nameof(customer.Created), "Customer cannot be from the future");

        if (string.IsNullOrWhiteSpace(billingAddress.LineOne))
            errors.Add(nameof(billingAddress.LineOne), "First address line is required");

        // DRY - Dont repeat yourself, post code validation is present multiple times (for billing address and for shipping address, consider extraction into a helper method 
        if (string.IsNullOrWhiteSpace(billingAddress.PostCode))
            errors.Add(nameof(billingAddress.PostCode), "PostCode is required");

        if (!Regex.IsMatch(billingAddress.PostCode,
                           @"^(GIR 0AA|((([A-Z]{1,2}[0-9][0-9A-Z]?)|(([A-Z]{1,2}[0-9][0-9A-Z]?)))(\s?[0-9][A-Z]{2})))$",
                           RegexOptions.IgnoreCase))
            errors.Add(nameof(billingAddress.PostCode), "Postcode is not valid");

        if (string.IsNullOrWhiteSpace(shippingAddress.LineOne))
            errors.Add(nameof(shippingAddress.LineOne), "First address line is required");

        if (string.IsNullOrWhiteSpace(shippingAddress.PostCode))
            errors.Add(nameof(shippingAddress.PostCode), "PostCode is required");

        if (!Regex.IsMatch(shippingAddress.PostCode,
                           @"^(GIR 0AA|((([A-Z]{1,2}[0-9][0-9A-Z]?)|(([A-Z]{1,2}[0-9][0-9A-Z]?)))(\s?[0-9][A-Z]{2})))$",
                           RegexOptions.IgnoreCase))
            errors.Add(nameof(shippingAddress.PostCode), "Postcode is not valid");

        return !errors.Any();
    }
}