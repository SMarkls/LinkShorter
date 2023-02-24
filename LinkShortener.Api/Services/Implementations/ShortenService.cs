using System.Net;
using LinkShortener.Api.Data.DataBase;
using LinkShortener.Api.Models;
using LinkShortener.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LinkShortener.Api.Services.Implementations;

public class ShortenService : IShortenService
{
    private readonly IHashCalculator calculator;
    private readonly ApiDbContext context;

    public ShortenService(IHashCalculator calculator, ApiDbContext context)
    {
        this.calculator = calculator;
        this.context = context;
    }
    public async Task<BaseResponse<bool>> CreateTokenAsync(string link, int ownerId)
    {
        string token = calculator.GetHash(link).Remove(6);
        while (await context.Links.FirstOrDefaultAsync(u => u.Token == token) != null)
        {
            
        }

        ShortenLinkModel shortenLink = new ShortenLinkModel
        {
            Created = DateTime.Now,
            Token = token,
            Link = link,
            OwnerId = ownerId
        };
        await context.Links.AddAsync(shortenLink);
        await context.SaveChangesAsync();
        return new BaseResponse<bool>
        {
            Data = true,
            Description = token,
            StatusCode = HttpStatusCode.Created
        };
    }

    public async Task<BaseResponse<string>> GetLinkAsync(string token)
    {
        var link = await context.Links.FirstOrDefaultAsync(l => l.Token == token);
        if (link == null)
            return new BaseResponse<string>
            {
                Data = string.Empty,
                Description = "Not Found",
                StatusCode = HttpStatusCode.NotFound
            };
        link.CountOfRedirects++;
        await context.SaveChangesAsync();
        return new BaseResponse<string>
        {
            Data = link.Link,
            Description = "Successfully Found",
            StatusCode = HttpStatusCode.OK
        };
    }

    public async Task<IEnumerable<ShortenLinkModel>> GetLinksAsync(int id)
    {
        return await context.Links.Where(x => x.OwnerId == id).ToArrayAsync();
    }

    public async Task<BaseResponse<bool>> DeleteLink(int id, int ownerId)
    {
        var link = await context.Links.FirstOrDefaultAsync(l => l.Id == id);
        if (link == null || link.OwnerId != ownerId)
            return new BaseResponse<bool>
            {
                Data = false,
                Description = "No Access or Not found.",
                StatusCode = HttpStatusCode.NotFound
            };
        
        context.Links.Remove(link);
        await context.SaveChangesAsync();
        return new BaseResponse<bool>
        {
            Data = true,
            Description = "Successfully deleted.",
            StatusCode = HttpStatusCode.OK
        };
    }
}