using System.Security.Cryptography;
using System.Text;
using LinkShortener.Api.Services.Interfaces;

namespace LinkShortener.Api.Services.Implementations;

public class HashCalculator : IHashCalculator
{
    public string GetHash(string str)
    {
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(str));
        var hash = BitConverter.ToString(bytes).Replace("-", "").ToLower();
        return hash;
    }
}