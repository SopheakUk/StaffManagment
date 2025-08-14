using StaffManagementCore.Model;

namespace StaffManagementAPI.Service.Interface;

public interface IStaffService
{
    Task<IdResponse> Add(StaffModel model);

    Task<IdResponse> Delete(long id);

    Task<QueryResponse<StaffModel>> Query(StaffQueryRequest query);

    Task<IdResponse> Update(StaffModel model);
}