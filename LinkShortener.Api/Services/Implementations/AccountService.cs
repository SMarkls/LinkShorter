using System.Net;
using LinkShortener.Api.Data.DataBase;
using LinkShortener.Api.Models;
using LinkShortener.Api.Models.UserModels;
using LinkShortener.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Api.Services.Implementations;

public class AccountService : IAccountService
{
    private readonly ApiDbContext context;
    private readonly IHashCalculator calculator;

    public AccountService(ApiDbContext context, IHashCalculator calculator)
    {
        this.context = context;
        this.calculator = calculator;
    }
    public async Task<BaseResponse<bool>> IsCorrectPasswordAsync(string login, string password)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Login == login);
        if (user == null)
            return new BaseResponse<bool>
            {
                Data = false,
                Description = "User Not Found",
                StatusCode = HttpStatusCode.OK
            };
        var hash = calculator.GetHash(password);
        if (hash == user.HashPassword)
            return new BaseResponse<bool>
            {
                Data = true,
                Description = "Ok",
                StatusCode = HttpStatusCode.OK
            };
        return new BaseResponse<bool>
        {
            Data = false,
            Description = "Incorrect Password",
            StatusCode = HttpStatusCode.OK
        };
    }

    public async Task<BaseResponse<bool>> RegisterUserAsync(string login, string password)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Login == login);
        if (user != null)
            return new BaseResponse<bool>
            {
                Data = false,
                Description = "User already exists",
                StatusCode = HttpStatusCode.Ambiguous
            };
        var hash = calculator.GetHash(password);
        user = new UserModel
        {
            Login = login,
            HashPassword = hash
        };
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return new BaseResponse<bool>
        {
            Data = true,
            Description = "User Created",
            StatusCode = HttpStatusCode.Created
        };
    }
}