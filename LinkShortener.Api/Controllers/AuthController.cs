using LinkShortener.Api.Models;
using LinkShortener.Api.Models.UserModels;
using LinkShortener.Api.Services.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace LinkShortener.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController
{
    private readonly IAccountService service;

    public AuthController(IAccountService service)
    {
        this.service = service;
    }
    [HttpPost]
    [Route("[action]")]
    public async Task<BaseResponse<int>> Register(RegisterModel model)
    {
        return await service.RegisterUserAsync(model.Login, model.Password);
    }
    [HttpPost]
    [Route("[action]")]
    public async Task<BaseResponse<bool>> Login(LoginModel model)
    {
        return await service.IsCorrectPasswordAsync(model.Login, model.Password);
    }
}