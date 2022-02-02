using Loans.Frontend.Models;
using System.Net.Http.Json;
using Loans.Frontend.Service.Contracts;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Loans.Frontend.Service
{
    public abstract class BaseApiService
    {
        protected HttpClient HttpClient { get; set; }

        protected INotificationService NotificationService { get; set; }

        protected BaseApiService(HttpClient httpClient, INotificationService notificationService)
        {
            HttpClient = httpClient;
            NotificationService = notificationService;
        }

        protected async Task<HttpSuccessResponseModel> MakePostRequest<T>(string endpoint, T requestModel, string authorizationToken = null)
        {
            try
            {
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, endpoint);

                if (authorizationToken != null)
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken);
                }

                request.Content = new StringContent(JsonSerializer.Serialize(requestModel), Encoding.UTF8, "application/json");


                using (HttpResponseMessage response = await HttpClient.SendAsync(request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        HttpResponseModel responseModel = await response.Content.ReadFromJsonAsync<HttpResponseModel>();

                        return new HttpSuccessResponseModel
                        {
                            HttpResponseModel = responseModel,
                            Success = true
                        };
                    }

                    return new HttpSuccessResponseModel
                    {
                        HttpResponseModel = null,
                        Success = false
                    };
                }
            }
            catch (Exception)
            {
                return new HttpSuccessResponseModel { HttpResponseModel = null, Success = false };
            }
        }

        protected Task<HttpSuccessResponseModel> MakeGetRequest<T>(string endpoint, Dictionary<string, T> requestParams, string authorizationToken = null)
        {
            if (requestParams != null)
            {
                string parameters = string.Join('&', requestParams.Select(p => $"{p.Key}={p.Value}"));

                return MakeGetRequest($"{endpoint}?{parameters}", authorizationToken);
            } else
            {
                return MakeGetRequest(endpoint, authorizationToken);
            }
        }
        
        protected async Task<HttpSuccessResponseModel> MakeGetRequest(string endpoint, string authorizationToken = null)
        {
            try
            {
                HttpRequestMessage request;

                request = new HttpRequestMessage(HttpMethod.Get, endpoint);
                
                if (authorizationToken != null)
                {
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authorizationToken);
                }

                using (HttpResponseMessage response = await HttpClient.SendAsync(request))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        HttpResponseModel responseModel = await response.Content.ReadFromJsonAsync<HttpResponseModel>();

                        return new HttpSuccessResponseModel
                        {
                            HttpResponseModel = responseModel,
                            Success = true
                        };
                    }
                }

                return new HttpSuccessResponseModel
                {
                    HttpResponseModel = null,
                    Success = false
                };
            }
            catch (Exception)
            {
                return new HttpSuccessResponseModel { HttpResponseModel = null, Success = false };
            }
        }
    }
}
