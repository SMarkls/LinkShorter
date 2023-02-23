using System.Text.Json;
using LinkShortener.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortener.Mvc.Controllers;

public class RedirectController : Controller
{
    [HttpGet("{token:alpha}")]
    public async Task<IActionResult> RedirectToToken(string token)
    {
        using var client = new HttpClient();
        var response = await client.GetAsync($"http://localhost:5255/api/Shorten/GetLink/{token}");
        BaseResponse<string>? parsedResponse = await JsonSerializer.DeserializeAsync<BaseResponse<string>>(await response.Content.ReadAsStreamAsync());
        if (parsedResponse != null)
            return Redirect($"{parsedResponse.Data}");
        return RedirectToAction("Index", "Home");
    }
}