using Loans.Domain.Models;
using Newtonsoft.Json;

namespace Loans.Domain.Maps
{
    public static class ResponseModelMapping
    {
        public static string ToJson(this HttpResponseModel model) => 
            JsonConvert.SerializeObject(model);
    }
}
