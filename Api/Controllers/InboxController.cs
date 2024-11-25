using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class InboxController : ControllerBase
{
    [HttpPost("[action]")]
    public IActionResult Receive(object payload)
    {
        throw new NotImplementedException();

        // this is a placeholder to represent we would receive incoming messages
        // a key example is product price and stock updates
    }
}
