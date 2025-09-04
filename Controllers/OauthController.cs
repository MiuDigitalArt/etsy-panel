using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace MiuDigitalArt.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OauthController: ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _config;

    public OauthController(IHttpClientFactory httpClientFactory, IConfiguration config)
    {
        _httpClientFactory = httpClientFactory;
        _config = config;
    }

    [HttpGet("callback")]
    public async Task<IActionResult> Callback([FromQuery] string code, [FromQuery] string state)
    {
        if (string.IsNullOrEmpty(code))
            return BadRequest("Missing authorization code");

        // Etsy token endpoint
        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.etsy.com/v3/public/oauth/token");

        var parameters = new Dictionary<string, string>
        {
            ["grant_type"] = "authorization_code",
            ["client_id"] = _config["Etsy:AccessToken"],
            ["redirect_uri"] = _config["Etsy:RedirectUri"],
            ["code"] = code
        };

        request.Content = new FormUrlEncodedContent(parameters);
        request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

        var client = _httpClientFactory.CreateClient();
        var response = await client.SendAsync(request);

        var content = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            return StatusCode((int)response.StatusCode, content);

        // İstersen content'i parse edip refresh_token vs. saklayabilirsin
        return Ok("Etsy OAuth başarılı. Token alındı:\n" + content);
    }
}