using System.Net;

namespace LinkShortener.Api.Models;

public class BaseResponse<T>
{
    public string Description { get; set; }
    public HttpStatusCode StatusCode { get; set; }
    public T Data { get; set; }
}