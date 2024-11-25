using Api.SwaggerExamples;
using Application.Exceptions;
using Application.Orders;
using Client.Dtos;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderCreator _orderCreator;
    private readonly IOrderUpdater _orderUpdater;

    public OrdersController(IOrderCreator orderCreator,
                            IOrderUpdater orderUpdater)
    {
        _orderCreator = orderCreator;
        _orderUpdater = orderUpdater;
    }


    [HttpPost("[action]")]
    [SwaggerRequestExample(typeof(CreateOrderRequestDto), typeof(CreateOrderExample))]
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

    [HttpPost("[action]")]
    [SwaggerRequestExample(typeof(UpdateOrderRequestDto), typeof(UpdateOrderExample))]
    public Task<IActionResult> Update([FromBody] UpdateOrderRequestDto request)
    {
        try
        {
            var response = _orderUpdater.UpdateOrder(request);

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
