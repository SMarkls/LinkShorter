using System.Collections;
using LinkShortener.Api.Models;

namespace LinkShortener.Api.Services.Interfaces;

public interface IShortenService
{
    public Task<BaseResponse<bool>> CreateTokenAsync(string link, int ownerId);
    public Task<BaseResponse<string>> GetLinkAsync(string token);
    public Task<IEnumerable<ShortenLinkModel>> GetLinksAsync(int id);
}