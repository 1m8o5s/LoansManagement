using Loans.Domain.Models;

namespace Loans.Helpers.Response.Contracts
{
    public interface IHttpResponseModelFactory
    {
        public HttpResponseModel NewSuccessResponse(object data, string message="");

        public HttpResponseModel NewErrorResponse(string message);
    }
}
