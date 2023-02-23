using System.Net;
using LinkShortener.Api.Models;
using LinkShortener.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LinkShortener.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ShortenController : ControllerBase
{
    private readonly IShortenService shortenService;

    public ShortenController(IShortenService shortenService)
    {
        this.shortenService = shortenService;
    }

    [HttpPost]
    [Route("[action]")]
    public async Task<BaseResponse<bool>> CreateLink(CreateLinkModel model)
    {
        if (Request.Cookies.TryGetValue("ownerId", out string id))
        {
            return await shortenService.CreateTokenAsync(model.Link, int.Parse(id));
        }
        return new BaseResponse<bool>
        {
            Data = false,
            StatusCode = HttpStatusCode.Unauthorized,
            Description = "Not Authorized"
        };
    }

    [HttpGet]
    [Route("[action]/{token}")]
    public async Task<BaseResponse<string>> GetLink(string token)
    {
        return await shortenService.GetLinkAsync(token);
    }
}