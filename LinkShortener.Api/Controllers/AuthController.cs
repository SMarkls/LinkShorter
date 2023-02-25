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
    /// <summary>
    /// Принимает <see cref="RegisterModel"/> в качестве параметра. Осуществляет регистрацию по логину и паролю.
    /// </summary>
    /// <param name="model"></param>
    /// <returns>В случае успешной регистрации возвращает <see cref="BaseResponse{T}"/> с Id пользователя. Иначе -1.</returns>
    [HttpPost]
    [Route("[action]")]
    public async Task<BaseResponse<int>> Register(RegisterModel model)
    {
        return await service.RegisterUserAsync(model.Login, model.Password);
    }
    
    /// <summary>
    /// Принимает <see cref="LoginModel"/> в качестве параметра. Проводит проверку на соответствие логина и пароля.
    /// </summary>
    /// <param name="model"></param>
    /// <returns>В случае соответствия возвращает <see cref="BaseResponse{T}"/> с Id пользователя. Иначе -1.</returns>
    [HttpPost]
    [Route("[action]")]
    public async Task<BaseResponse<int>> Login(LoginModel model)
    {
        return await service.IsCorrectPasswordAsync(model.Login, model.Password);
    }
}