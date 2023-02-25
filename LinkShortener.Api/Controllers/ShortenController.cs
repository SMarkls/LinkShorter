using System.Net;
using LinkShortener.Api.Models;
using LinkShortener.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

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
    
    /// <summary>
    /// Создает сокращенную ссылку для указанной в теле запроса, требует заголовок ownerId в запросе со значением
    /// id пользователя.
    /// </summary>
    /// <param name="model"></param>
    /// <returns><see cref="BaseResponse{T}"/> с токеном короткой ссылки в свойстве Description.</returns>
    [HttpPost]
    [Route("[action]")]
    public async ValueTask<BaseResponse<bool>> CreateLink(CreateLinkModel model)
    {
        if (Request.Headers.TryGetValue("ownerId", out StringValues id))
        {
            return await shortenService.CreateTokenAsync(model.Link, int.Parse(id[0]!));
        }
        return new BaseResponse<bool>
        {
            Data = false,
            StatusCode = HttpStatusCode.Unauthorized,
            Description = "Not Authorized"
        };
    }
    
    /// <summary>
    /// Возвращает полную ссылку по токену, переданному в параметре.
    /// </summary>
    /// <param name="token"></param>
    /// <returns><see cref="BaseResponse{T}"/> с полной ссылкой.</returns>
    [HttpGet]
    [Route("[action]/{token}")]
    public async Task<BaseResponse<string>> GetLink(string token)
    {
        return await shortenService.GetLinkAsync(token);
    }

    /// <summary>
    /// Возвращает все ссылки, созданные пользователем. Требует заголовок ownerId в запросе со значением id пользователя.
    /// </summary>
    /// <returns><see cref="BaseResponse{T}"/> с последовательностью <see cref="ShortenLinkModel"/>.</returns>
    [HttpGet]
    [Route("[action]")]
    public async ValueTask<BaseResponse<IEnumerable<ShortenLinkModel>>> GetLinks()
    {
        if (Request.Headers.TryGetValue("ownerId", out StringValues id))
        {
            return await shortenService.GetLinksAsync(int.Parse(id[0]!));
        }

        return new BaseResponse<IEnumerable<ShortenLinkModel>>()
        {
            Data = Enumerable.Empty<ShortenLinkModel>(),
            Description = "You are unauthorized",
            StatusCode = HttpStatusCode.Unauthorized
        };
    }
    
    /// <summary>
    /// Удаляет ссылку из базы данных. Требует заголовок ownerId в теле запроса со значением id пользователя.
    /// </summary>
    /// <param name="linkId"></param>
    /// <returns><see cref="BaseResponse{T}"/> с <see cref="bool"/>, означающим успех или провал операции.</returns>
    [HttpDelete]
    [Route("[action]/{linkId:int}")]
    public async ValueTask<BaseResponse<bool>> DeleteLink(int linkId)
    {
        if (Request.Headers.TryGetValue("ownerId", out StringValues id))
        {
            return await shortenService.DeleteLink(linkId, int.Parse(id[0]!));
        }
        return new BaseResponse<bool>
        {
            Data = false,
            StatusCode = HttpStatusCode.Unauthorized,
            Description = "You are unauthorized."
        };
    }
}