using System.ComponentModel.DataAnnotations;

namespace LinkShortener.Mvc.Models;

public class LinkModel
{
    // проверка на то, является ли строка ссылкой
    [RegularExpression(@"[https://]*[\w\d]+\.+[\w\d]+/*[\w\d/\.]*", ErrorMessage = "Строка не является ссылкой!")]
    [Required(ErrorMessage = "Введите ссылку!")]
    public string Link { get; set; }
}