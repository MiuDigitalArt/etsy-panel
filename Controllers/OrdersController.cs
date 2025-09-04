using Microsoft.AspNetCore.Mvc;
using MiuDigitalArt.Services;

namespace MiuDigitalArt.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController: ControllerBase
{
    private readonly EtsyClient _etsy;

    public OrdersController(EtsyClient etsy) => _etsy = etsy;

    [HttpGet]
    public async Task<IActionResult> Get() =>
        Ok(await _etsy.GetOrdersAsync());
}