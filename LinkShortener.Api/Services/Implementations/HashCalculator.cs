using System.Security.Cryptography;
using System.Text;
using LinkShortener.Api.Services.Interfaces;

namespace LinkShortener.Api.Services.Implementations;

public class HashCalculator : IHashCalculator
{
    public string GetPasswordHash(string password)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
        var hash = BitConverter.ToString(bytes).Replace("-", "").ToLower();
        return hash;
    }

    public string GetLinkToken(string link)
    {
        return Convert.ToBase64String(Encoding.UTF8.GetBytes(Guid.NewGuid().ToString())).ToLower().Remove(6);
    }
}