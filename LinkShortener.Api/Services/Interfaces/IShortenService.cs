using LinkShortener.Api.Models;

namespace LinkShortener.Api.Services.Interfaces;

public interface IShortenService
{
    /// <summary>
    /// Создаёт токен для короткой ссылки.
    /// </summary>
    /// <param name="link">Полная ссылка.</param>
    /// <param name="ownerId">Id пользователя.</param>
    /// <returns><see cref="BaseResponse{T}"/> с успехом или провалом операции, и с токеном в свойстве Description</returns>
    public Task<BaseResponse<bool>> CreateTokenAsync(string link, int ownerId);
    
    /// <summary>
    /// Возвращает полную ссылку по токену.
    /// </summary>
    /// <param name="token">Токен короткой ссылки.</param>
    /// <returns><see cref="BaseResponse{T}"/> с полной ссылкой.</returns>
    public Task<BaseResponse<string>> GetLinkAsync(string token);
    
    /// <summary>
    /// Возвращает последовательность всех ссылок созданных пользователем.
    /// </summary>
    /// <param name="ownerId">Id пользователя.</param>
    /// <returns><see cref="BaseResponse{T}"/> с последовательностью всех ссылок, созданных пользователем.</returns>
    public Task<BaseResponse<IEnumerable<ShortenLinkModel>>> GetLinksAsync(int ownerId);
    
    /// <summary>
    /// Удаляет ссылку и токен.
    /// </summary>
    /// <param name="id">Id ссылки.</param>
    /// <param name="ownerId">Id пользователя.</param>
    /// <returns><see cref="BaseResponse{T}"/> с успехом или провалом операции.</returns>
    public Task<BaseResponse<bool>> DeleteLink(int id, int ownerId);
}