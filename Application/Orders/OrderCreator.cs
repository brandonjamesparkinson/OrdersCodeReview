using Application.Addresses;
using Application.Customers;
using Client.Dtos;
using DataAccess;
using Domain;

namespace Application.Orders;

public class OrderCreator(
    ICustomerProvider customerProvider,
    IAddressProvider addressProvider,
    ICreateOrderRequestValidator requestValidator,
    IRepository<Order> orderRepo,
    IRepository<Variant> variantRepo,
    IUnitOfWork unitOfWork)
    : IOrderCreator
{
    private readonly ICustomerProvider customerProvider = customerProvider;
    private readonly IAddressProvider addressProvider = addressProvider;
    private readonly IRepository<Order> _orderRepo = orderRepo;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public OrderDto CreateOrder(CreateOrderRequestDto request)
    {
        var customer = customerProvider.GetCustomer(request.Customer);
        var addresses = addressProvider.GetAddresses(request.Customer.BillingAddress,
                                                     request.Customer.ShippingAddress);

        if (!requestValidator.TryValidate(customer, addresses.Item1, addresses.Item2, out var errors))
            throw new ValidationException("Validation failed", errors);

        var order = new Order(customer,
                              addresses.Item1,
                              addresses.Item2);

        _orderRepo.Insert(order);

        var products = GetProducts(request.OrderItems);

        order.UpdateItems(products);

        _unitOfWork.Save();

        return new OrderDto();
    }

    private IDictionary<Variant, int> GetProducts(ICollection<CreateOrderItemDto> items)
    {
        var variants = new HashSet<Variant>();
        foreach (var item in items)
        {
            var variant = variantRepo.Get(x => x.Sku == item.Sku).Single();
            variants.Add(variant);
        }

        var requestedSkus = items.Select(x => x.Sku);

        var missingSkus = requestedSkus.ExceptBy(variants.Select(x => x.Sku), sku => sku).ToList();

        if (missingSkus.Any())
            throw new ValidationException("Request failed validation",
                                          new Dictionary<string, string>()
                                          {
                                              [nameof(missingSkus)] = string.Join(',', missingSkus)
                                          });

        return variants.Join(items,
                             x => x.Sku.ToUpperInvariant(),
                             x => x.Sku.ToUpperInvariant(),
                             (variant,
                              requestItem) => new
                              {
                                  variant,
                                  requestItem
                              })
                       .ToDictionary(x => x.variant, x => x.requestItem.Quantity);
    }
}