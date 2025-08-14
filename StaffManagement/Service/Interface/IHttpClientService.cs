using StaffManagementCore.Model;
using System.Text.Json;

namespace StaffManagement.Service.Interface;

public interface IHttpClientService
{
    Task<TResponse> DeleteAsync<TResponse>(string uri) where TResponse : ResponseBase;

    Task<TResponse> GetAsync<TResponse>(string uri) where TResponse : ResponseBase;

    Task<TResponse> GetFromJsonAsync<TResponse>(string uri, CancellationToken cancellationToken = default);

    Task<TResponse> PostAsJsonAsync<TResponse, TRequest>(string uri, TRequest request, JsonSerializerOptions options = null, CancellationToken cancellationToken = default) where TResponse : ResponseBase;

    Task<TResponse> PostAsync<TResponse>(string uri, HttpContent httpContent = null) where TResponse : ResponseBase;

    Task<TResponse> PutAsJsonAsync<TResponse, TRequest>(string uri, TRequest request, JsonSerializerOptions options = null) where TResponse : ResponseBase;

    Task<QueryResponse<TResponse>> QueryAsync<TResponse, TRequest>(string uri, TRequest request, JsonSerializerOptions options = null, CancellationToken cancellationToken = default);
}