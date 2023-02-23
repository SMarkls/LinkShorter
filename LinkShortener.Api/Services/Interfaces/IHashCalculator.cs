namespace LinkShortener.Api.Services.Interfaces;

public interface IHashCalculator
{
    public string GetHash(string str);
}