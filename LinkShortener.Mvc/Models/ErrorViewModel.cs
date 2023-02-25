using System.Net;

namespace LinkShortener.Mvc.Models;

public class ErrorViewModel
{
    public ErrorViewModel(string desc, HttpStatusCode? code = null)
    {
        Code = code;
        ErrorDescription = desc;
    }
    public string ErrorDescription { get; set; }
    public HttpStatusCode? Code { get; set; }
}