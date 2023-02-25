using LinkShortener.Api.Models;

namespace LinkShortener.Api.Services.Interfaces;

public interface IAccountService
{
    /// <summary>
    /// Принимает на вход логин и пароль, проверяет их соответствие.
    /// </summary>
    /// <param name="login">Логин аккаунта.</param>
    /// <param name="password">Пароль аккаунта.</param>
    /// <returns><see cref="BaseResponse{T}"/> с Id пользователя в случае соответствия. Иначе -1.</returns>
    public Task<BaseResponse<int>> IsCorrectPasswordAsync(string login, string password);
    /// <summary>
    /// Принимает на вход логин и пароль, создает в базе данных аккаунт пользователя, если логин не занят.
    /// </summary>
    /// <param name="login">Логин аккаунта.</param>
    /// <param name="password">Пароль аккаунта.</param>
    /// <returns><see cref="BaseResponse{T}"/> с Id зарегистрированного пользователя.</returns>
    public Task<BaseResponse<int>> RegisterUserAsync(string login, string password);
}