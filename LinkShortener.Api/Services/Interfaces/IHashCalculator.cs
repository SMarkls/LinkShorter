namespace LinkShortener.Api.Services.Interfaces;

public interface IHashCalculator
{
    /// <summary>
    /// Вычисляет хэш пароля.
    /// </summary>
    /// <param name="password"></param>
    /// <returns></returns>
    public string GetPasswordHash(string password);

    /// <summary>
    /// Возвращает хэш для ссылки.
    /// </summary>
    /// <param name="link">Полная ссылка</param>
    /// <returns>Шестисзначный токен короткой ссылки.</returns>
    public string GetLinkToken(string link);
}