using LinkShortener.Api.Models;

namespace LinkShortener.Api.Services.Interfaces;

public interface IAccountService
{
    public Task<BaseResponse<bool>> IsCorrectPasswordAsync(string login, string password);
    public Task<BaseResponse<bool>> RegisterUserAsync(string login, string password);
}