using Loans.Domain.Models;
using Loans.Helpers.Response.Contracts;

namespace Loans.Helpers.Response
{
    public class HttpResponseModelFactory : IHttpResponseModelFactory
    {
        public HttpResponseModel NewSuccessResponse(object data, string message = "") =>
            new HttpResponseModel
            {
                Data = data,
                Message = message,
                Status = System.Net.HttpStatusCode.OK
            };

        public HttpResponseModel NewErrorResponse(string message) =>
            new HttpResponseModel
            {
                Message = message,
                Status = System.Net.HttpStatusCode.BadRequest,
                Data = null
            };
    }
}
