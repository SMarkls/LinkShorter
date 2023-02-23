using System.ComponentModel.DataAnnotations;

namespace LinkShortener.Api.Models.UserModels;

public class LoginModel
{
    [MaxLength(50)]
    [MinLength(3)]
    public string Login { get; set; }
    [MaxLength(60)]
    [MinLength(6)]
    public string Password { get; set; }
}