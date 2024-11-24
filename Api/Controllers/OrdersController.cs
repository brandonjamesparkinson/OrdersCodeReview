using Application.Exceptions;
using Application.Orders;
using Client.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderCreator _orderCreator;

    public OrdersController(IOrderCreator orderCreator)
    {
        _orderCreator = orderCreator;
    }


    [HttpPost("[action]")]
    public Task<IActionResult> Create([FromBody]CreateOrderRequestDto request)
    {
        try
        {
            var response = _orderCreator.CreateOrder(request);

            return Task.FromResult<IActionResult>(Ok(response));
        }
        catch (ValidationException ex)
        {
            return Task.FromResult<IActionResult>(BadRequest(ex.Errors));
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}
