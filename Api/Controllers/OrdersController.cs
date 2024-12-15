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
    private readonly IOrderReader _orderReader;
    private readonly IOrderCreator _orderCreator;
    private readonly IOrderUpdater _orderUpdater;

    public OrdersController(IOrderReader orderReader,
                            IOrderCreator orderCreator,
                            IOrderUpdater orderUpdater)
    {
        _orderReader = orderReader;
        _orderCreator = orderCreator;
        _orderUpdater = orderUpdater;
    }

    // Currently not async/await, relying on Task.FromResult. 
    // If this is async (often with db calls - note using sqlite instead here) I would potentially change this to an async task and await the response
    // Would suggest more consistent 'Routing' patterns...e.g. This is Getting Order Number
    //[HttpGet("{orderNumber}")]
    [HttpGet("[action]")]
    //public Task<IActionResult> GetOrder([FromQuery] string orderNumber)
    public Task<IActionResult> Get([FromQuery]string orderNumber) // Suggest clearer naming of methods 
    {
        try
        {
            //var response = await _orderReader.ReadOrderAsync(orderNumber);
            //return Ok(response);
            var response = _orderReader.ReadOrder(orderNumber);

            return Task.FromResult<IActionResult>(Ok(response));
        }
        // I like how there are different exceptions here, I think it could help an end user using or a dev during debugging
        // I would suggest improving this, as rethrowing can cause objects to lose original stack trace. https://learn.microsoft.com/en-us/dotnet/fundamentals/code-analysis/quality-rules/ca2200
        catch (ValidationException ex)
        {
            return Task.FromResult<IActionResult>(BadRequest(ex.Errors));
        }
        catch (Exception ex)
        {
            // 'Exception' is too generic, if you know the cause then you can return a specific one (discussed below), if invalid input, BadRequest with details etc.
            throw new Exception(ex.Message);

            // BP / 15/12/2024 - Suggestion: Pass the order number into the error message for end user use with context to WHY it fell over, e.g. does not exist 
            // Order Not Found Exception 
            // Catch (OrderNotFoundException) // custom exception we define 
            //throw new Exception($"No order found for order number: {orderNumber}", ex);
        }
    }

    // Same feedback on method name, createOrder etc, HttpPost would have routing relevant to what it's doing 
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

    // Same feedback on method name, createOrder etc, HttpPost would have routing relevant to what it's doing 
    [HttpPut("[action]")]
    [SwaggerRequestExample(typeof(UpdateOrderRequestDto), typeof(UpdateOrderExample))]
    public Task<IActionResult> Update([FromBody] UpdateOrderRequestDto request)
    {
        try
        {
            // breaks here - due to null order on updating 
            var response = _orderUpdater.UpdateOrder(request);

            return Task.FromResult<IActionResult>(Ok(response));
        }
        catch (ValidationException ex)
        {
            return Task.FromResult<IActionResult>(BadRequest(ex.Errors));
        }
        catch (Exception ex)
        {
            // order was null - suggest clearer exception handling where possible here 
            throw new Exception(ex.Message);
        }
    }
}
