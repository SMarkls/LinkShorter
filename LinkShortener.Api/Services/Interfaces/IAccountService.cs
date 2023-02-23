using LinkShortener.Api.Models;

namespace LinkShortener.Api.Services.Interfaces;

public interface IAccountService
{
    public Task<BaseResponse<bool>> IsCorrectPasswordAsync(string login, string password);
    public Task<BaseResponse<int>> RegisterUserAsync(string login, string password);
}