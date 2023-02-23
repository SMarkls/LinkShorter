using System.Text.Json;
using LinkShortener.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortener.Mvc.Controllers;

public class RedirectController : Controller
{
    [HttpGet("{token}")]
    public async ValueTask<IActionResult> RedirectToToken(string token)
    {
        if (token.Length != 6)
            return RedirectToAction("Index", "Home");
        using var client = new HttpClient();
        var response = await client.GetAsync($"http://localhost:5255/api/Shorten/GetLink/{token}");
        BaseResponse<string>? parsedResponse = await JsonSerializer.DeserializeAsync<BaseResponse<string>>(
            await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions(JsonSerializerDefaults.Web));
        if (parsedResponse != null)
            return Redirect(parsedResponse.Data.Contains("https://")
                ? $"{parsedResponse.Data}"
                : $"https://{parsedResponse.Data}");
        return RedirectToAction("Index", "Home");
    }
}