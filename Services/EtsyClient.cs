using System.Net.Http.Headers;
using System.Text.Json;

namespace MiuDigitalArt.Services;

public class EtsyClient
{
    private readonly HttpClient _httpClient;
    private readonly string _baseUrl = "https://openapi.etsy.com/v3/application";
    private readonly string _shopId;

    public EtsyClient(IConfiguration config)
    {
        _shopId = config["Etsy:ShopId"]!;
        _httpClient = new HttpClient();
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", config["Etsy:AccessToken"]);
    }

    private async Task<JsonElement?> GetAsync(string endpoint)
    {
        var response = await _httpClient.GetAsync($"{_baseUrl}/{endpoint}");
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<JsonElement>(json);
    }

    public async Task<JsonElement?> GetShopAsync() =>
        await GetAsync($"shops/{_shopId}");

    public async Task<JsonElement?> GetProductsAsync() =>
        await GetAsync($"shops/{_shopId}/listings/active");

    public async Task<JsonElement?> GetOrdersAsync() =>
        await GetAsync($"shops/{_shopId}/receipts");
}