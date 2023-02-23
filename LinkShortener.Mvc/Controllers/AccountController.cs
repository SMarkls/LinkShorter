using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using LinkShortener.Api.Models;
using LinkShortener.Api.Models.UserModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortener.Mvc.Controllers;

public class AccountController : Controller
{
    public AccountController()
    {
        
    }
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async ValueTask<IActionResult> Login(LoginModel model)
    {
        if (!ModelState.IsValid)
            return View(model);
        using var client = new HttpClient();
        var response = await client.PostAsync("http://localhost:5255/api/Auth/Login",
            new StringContent(JsonSerializer.Serialize(model), new MediaTypeHeaderValue("application/json")));
        BaseResponse<int>? parsedResponse =
            await JsonSerializer.DeserializeAsync<BaseResponse<int>>(await response.Content.ReadAsStreamAsync(),
                new JsonSerializerOptions(JsonSerializerDefaults.Web));
        if (parsedResponse is { Data: > 0 })
        {
            var claims = new List<Claim>
            {
                new("User", model.Login),
                new("Id", parsedResponse.Data.ToString())
            };
            var claimIdentity = new ClaimsIdentity(claims, "Cookie");
            var claimPrincipal = new ClaimsPrincipal(claimIdentity);
            await HttpContext.SignInAsync("Cookie", claimPrincipal);
            Response.Cookies.Append("ownerId", $"{parsedResponse.Data}");
            return Redirect("/");
        }
        ViewBag.Error = parsedResponse?.Description!;
        return View("Login");
    }
    
    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public async ValueTask<IActionResult> Register(RegisterModel model)
    {
        if (!ModelState.IsValid)
            return View(model);
        using var client = new HttpClient();
        var response = await client.PostAsync("http://localhost:5255/api/Auth/Register",
            new StringContent(JsonSerializer.Serialize(model), new MediaTypeHeaderValue("application/json")));
        BaseResponse<int>? parsedResponse = await JsonSerializer.DeserializeAsync<BaseResponse<int>>(
            await response.Content.ReadAsStreamAsync(), new JsonSerializerOptions(JsonSerializerDefaults.Web));
        if (parsedResponse is { Data: > 0 })
        {
            var claims = new List<Claim>
            {
                new("User", model.Login),
                new("Id", parsedResponse.Data.ToString())
            };
            var claimIdentity = new ClaimsIdentity(claims, "Cookie");
            var claimPrincipal = new ClaimsPrincipal(claimIdentity);
            await HttpContext.SignInAsync("Cookie", claimPrincipal);
            Response.Cookies.Append("ownerId", $"{parsedResponse.Data}");
            return Redirect("/");
        }
        ViewBag.Error = parsedResponse?.Description!;
        return View("Register");
    }

    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        Response.Cookies.Delete("ownerId");
        return RedirectToAction("Login");
    }
}