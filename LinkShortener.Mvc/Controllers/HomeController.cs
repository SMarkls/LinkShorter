using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;
using LinkShortener.Api.Models;
using Microsoft.AspNetCore.Mvc;
using LinkShortener.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;

namespace LinkShortener.Mvc.Controllers;

public class HomeController : Controller
{
    [Authorize]
    public IActionResult Index()
    {
        return View();
    }
    [HttpPost]
    [Authorize]
    public async ValueTask<IActionResult> CreateLink(CreateLinkModel model)
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
                ViewBag.IsSuccess = $"Ссылка сгенерирована: {Request.GetDisplayUrl().Split("//")[1].Split("/")[0]}/{parsedResponse.Description}";
                return View("Index");
            }
        }
        
        return View("Index", model);
    }
    [HttpPost]
    public async Task<IActionResult> DeleteLink(DeleteLinkModel model)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("ownerId", HttpContext.User.Claims.First(x => x.Type == "Id").Value);
        var response = await client.DeleteAsync($"http://localhost:5255/api/Shorten/DeleteLink/{model.LinkId}");
        BaseResponse<bool>? parsedResponse =
            await JsonSerializer.DeserializeAsync<BaseResponse<bool>>(await response.Content.ReadAsStreamAsync(),
                new JsonSerializerOptions(JsonSerializerDefaults.Web));
        if (parsedResponse is { Data: true })
            return Redirect("/");
        return RedirectToAction("Error"); // TODO: сделать странцу с ошибкой.
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}