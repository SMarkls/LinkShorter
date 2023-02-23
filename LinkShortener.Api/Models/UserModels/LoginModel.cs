using System.ComponentModel.DataAnnotations;

namespace LinkShortener.Api.Models.UserModels;

public class LoginModel
{
    [MaxLength(50, ErrorMessage = "Длина логина должна быть менее 50 символов.")]
    [MinLength(3, ErrorMessage = "Длина логина должна быть больше 3 символов.")]
    public string Login { get; set; }
    [MaxLength(60, ErrorMessage = "Длина пароля должна быть мене 60 символов.")]
    [MinLength(6, ErrorMessage = "Длина пароля должна быть от 6 символов.")]
    public string Password { get; set; }
}