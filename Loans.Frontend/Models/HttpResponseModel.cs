using System.Net;

namespace Loans.Frontend.Models;

public class HttpResponseModel
{
    public object Data { get; set; }

    public string Message { get; set; }

    public HttpStatusCode Status { get; set; }
}