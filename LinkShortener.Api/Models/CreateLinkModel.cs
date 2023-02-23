using System.ComponentModel.DataAnnotations;

namespace LinkShortener.Api.Models;

public class CreateLinkModel
{
    [RegularExpression(@"[https://]*[\w\d]+\.+[\w\d]+/*[\w\d/\.]*", ErrorMessage = "Строка не является ссылкой!")]
    [Required(ErrorMessage = "Введите ссылку!")]
    public string Link { get; set; }
}