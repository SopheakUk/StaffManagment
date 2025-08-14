namespace StaffManagementCore.Model;

public class QueryResponse<T> : ResponseBase
{
    public List<T> Results { get; set; }
}