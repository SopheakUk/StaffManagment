using StaffManagement.Service.Interface;
using StaffManagementCore.Model;
using System.Text.Json;

namespace StaffManagement.Service;

public class HttpClientService(HttpClient _httpClient) : IHttpClientService
{
    public async Task<TResponse> PostAsync<TResponse>(string uri, HttpContent httpContent = null) where TResponse : ResponseBase
    {
        var result = await _httpClient.PostAsync(uri, httpContent);
        var response = await GetTResponseAsync<TResponse>(result);
        return response;
    }

    public async Task<TResponse> DeleteAsync<TResponse>(string uri) where TResponse : ResponseBase
    {
        var result = await _httpClient.DeleteAsync(uri);
        return await GetTResponseAsync<TResponse>(result);
    }

    public async Task<TResponse> GetFromJsonAsync<TResponse>(string uri, CancellationToken cancellationToken = default)
    {
        return await _httpClient.GetFromJsonAsync<TResponse>(uri, cancellationToken);
    }

    public async Task<TResponse> GetAsync<TResponse>(string uri) where TResponse : ResponseBase
    {
        var result = await _httpClient.GetAsync(uri);
        return await GetTResponseAsync<TResponse>(result);
    }

    public async Task<TResponse> PostAsJsonAsync<TResponse, TRequest>(string uri, TRequest request, JsonSerializerOptions options = null, CancellationToken cancellationToken = default) where TResponse : ResponseBase
    {
        var result = await _httpClient.PostAsJsonAsync(uri, request, options, cancellationToken);
        return await GetTResponseAsync<TResponse>(result);
    }

    public async Task<QueryResponse<TResponse>> QueryAsync<TResponse, TRequest>(string uri, TRequest request, JsonSerializerOptions options = null, CancellationToken cancellationToken = default)
    {
        var result = await _httpClient.PostAsJsonAsync(uri, request, options, cancellationToken);
        var response = await GetTResponseAsync<QueryResponse<TResponse>>(result);
        return response;
    }

    public async Task<TResponse> PutAsJsonAsync<TResponse, TRequest>(string uri, TRequest request, JsonSerializerOptions options = null) where TResponse : ResponseBase
    {
        var result = await _httpClient.PutAsJsonAsync(uri, request, options);
        return await GetTResponseAsync<TResponse>(result);
    }

    private static async Task<TResponse> GetTResponseAsync<TResponse>(HttpResponseMessage httpResponseMessage) where TResponse : ResponseBase
    {
        httpResponseMessage.EnsureSuccessStatusCode();

        var stream = await httpResponseMessage.Content.ReadAsStreamAsync();
        var responseBase = JsonSerializer.Deserialize<ResponseBase>(stream);
        responseBase.EnsureSuccessStatusCode();

        stream.Position = 0;
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        };
        return JsonSerializer.Deserialize<TResponse>(stream, options);
    }
}