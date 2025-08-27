using Microsoft.AspNetCore.Mvc;
using MiuDigitalArt.Services;

namespace MiuDigitalArt.Controllers;

[ApiController]
[Route("[controller]")]
public class ShopsController: ControllerBase
{
    private readonly EtsyClient _etsy;

    public ShopsController(EtsyClient etsy) => _etsy = etsy;

    [HttpGet]
    public async Task<IActionResult> Get() =>
        Ok(await _etsy.GetShopAsync());
}