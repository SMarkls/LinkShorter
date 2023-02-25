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
    public async Task<BaseResponse<int>> IsCorrectPasswordAsync(string login, string password)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Login == login);
        if (user == null)
            return new BaseResponse<int>
            {
                Data = -1,
                Description = "User Not Found",
                StatusCode = HttpStatusCode.OK
            };
        var hash = calculator.GetPasswordHash(password);
        if (hash == user.HashPassword)
            return new BaseResponse<int>
            {
                Data = user.Id,
                Description = "Ok",
                StatusCode = HttpStatusCode.OK
            };
        return new BaseResponse<int>
        {
            Data = -1,
            Description = "Incorrect Password",
            StatusCode = HttpStatusCode.OK
        };
    }

    public async Task<BaseResponse<int>> RegisterUserAsync(string login, string password)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Login == login);
        if (user != null)
            return new BaseResponse<int>
            {
                Data = -1,
                Description = "User already exists",
                StatusCode = HttpStatusCode.Ambiguous
            };
        var hash = calculator.GetPasswordHash(password);
        user = new UserModel
        {
            Login = login,
            HashPassword = hash
        };
        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
        return new BaseResponse<int>
        {
            Data = user.Id,
            Description = "User Created",
            StatusCode = HttpStatusCode.Created
        };
    }
}