using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LinkShortener.Api.Models.UserModels;

namespace LinkShortener.Api.Models;

public class ShortenLinkModel
{
    [Key]
    public int Id { get; set; }
    [MaxLength(100)]
    public string Link { get; set; }
    [MaxLength(6)]
    public string Token { get; set; }
    public DateTime Created { get; set; }
    public int CountOfRedirects { get; set; }
    [ForeignKey("Owner")]
    public int OwnerId { get; set; }
    public UserModel Owner { get; set; }
}