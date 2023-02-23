using System.Diagnostics;
using System.Text.Json;
using LinkShortener.Api.Models;
using Microsoft.AspNetCore.Mvc;
using LinkShortener.Mvc.Models;

namespace LinkShortener.Mvc.Controllers;

public class HomeController : Controller
{

    public HomeController()
    {
        
    }

    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> CreateLink(LinkModel model)
    {
        if (ModelState.IsValid)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Add("ownerId", "1");
            var response = await client.PostAsync("http://localhost:5255/api/Shorten/CreateLink",
                new StringContent($"\"link\": \"{model.Link}\""));
            BaseResponse<bool>? parsedResponse =
                await JsonSerializer.DeserializeAsync<BaseResponse<bool>>(await response.Content.ReadAsStreamAsync());
            if (parsedResponse is { Data: true })
            {
                ViewBag.IsSuccess = "Ссылка успешно создана!";
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