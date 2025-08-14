using StaffManagementCore.Model;

namespace StaffManagement.Service.Interface;

public interface IStaffService
{
    Task<ResponseBase> Delete(long id);
    Task<ResponseBase> Post(StaffModel model);
    Task<ResponseBase> Put(StaffModel model);
    Task<QueryResponse<StaffModel>> Query(StaffQueryRequest query);
}
