using System.ComponentModel.DataAnnotations;

namespace LinkShortener.Api.Models.UserModels;

public class UserModel
{
    [Key]
    public int Id { get; set; }
    [MaxLength(50)]
    public string Login { get; set; }
    [MaxLength(250)]
    public string HashPassword { get; set; }
}