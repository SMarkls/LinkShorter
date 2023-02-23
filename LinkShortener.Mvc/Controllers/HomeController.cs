using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using LinkShortener.Api.Models;
using Microsoft.AspNetCore.Mvc;
using LinkShortener.Mvc.Models;
using Microsoft.AspNetCore.Authorization;

namespace LinkShortener.Mvc.Controllers;

public class HomeController : Controller
{

    public HomeController()
    {
        
    }
    [Authorize]
    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateLink(CreateLinkModel model)
    {
        if (ModelState.IsValid)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("ownerId", "1");
            var response = await client.PostAsync("http://localhost:5255/api/Shorten/CreateLink",
                new StringContent(JsonSerializer.Serialize(model), new MediaTypeHeaderValue("application/json")));
            BaseResponse<bool>? parsedResponse =
                await JsonSerializer.DeserializeAsync<BaseResponse<bool>>(await response.Content.ReadAsStreamAsync(),
                    new JsonSerializerOptions(JsonSerializerDefaults.Web));
            if (parsedResponse is { Data: true })
            {
                ViewBag.IsSuccess = $"Ссылка сгенерирована: http://localhost:5255/{parsedResponse.Description}";
                return View("Index");
            }
        }
        
        return View("Index", model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}