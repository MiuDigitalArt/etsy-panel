using Microsoft.AspNetCore.Mvc;
using MiuDigitalArt.Services;

namespace MiuDigitalArt.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductsController : ControllerBase
{
    private readonly EtsyClient _etsy;

    public ProductsController(EtsyClient etsy) => _etsy = etsy;

    [HttpGet]
    public async Task<IActionResult> Get() =>
        Ok(await _etsy.GetProductsAsync());
}