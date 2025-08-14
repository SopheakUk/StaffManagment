using StaffManagement.Service.Interface;
using StaffManagementCore.Model;
using ModelX = StaffManagementCore.Model.StaffModel;

namespace StaffManagement.Service;

public class StaffService(IHttpClientService _httpClientService) : IStaffService
{
    private const string _url = "api/staff";

    public async Task<ResponseBase> Delete(long id)
    {
        var uri = $"{_url}/{id}";
        return await _httpClientService.DeleteAsync<ResponseBase>(uri);
    }

    public async Task<QueryResponse<ModelX>> Query(StaffQueryRequest query)
    {
        var uri = $"{_url}/query";
        return await _httpClientService.QueryAsync<ModelX, StaffQueryRequest>(uri, query);
    }

    public async Task<ResponseBase> Post(ModelX model)
    {
        var uri = _url;
        return await _httpClientService.PostAsJsonAsync<ResponseBase, ModelX>(uri, model);
    }

    public async Task<ResponseBase> Put(ModelX model)
    {
        var uri = _url;
        return await _httpClientService.PutAsJsonAsync<ResponseBase, ModelX>(uri, model);
    }
}