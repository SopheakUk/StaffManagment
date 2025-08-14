using Microsoft.AspNetCore.Mvc;
using StaffManagementAPI.Service.Interface;
using StaffManagementCore.Model;

namespace StaffManagementAPI.Api;

[Route("api/[controller]")]
[ApiController]
public class StaffController(IStaffService _staffService) : ControllerBase
{
    [HttpPost]
    public async Task<IdResponse> Post(StaffModel model) => await _staffService.Add(model);

    [HttpPost("Query")]
    public async Task<QueryResponse<StaffModel>> Query(StaffQueryRequest request) => await _staffService.Query(request);

    [HttpPut]
    public async Task<IdResponse> Put(StaffModel model) => await _staffService.Update(model);

    [HttpDelete("{id}")]
    public async Task<IdResponse> Delete(long id) => await _staffService.Delete(id);
}