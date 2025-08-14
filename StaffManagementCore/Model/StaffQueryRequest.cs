namespace StaffManagementCore.Model;

public class StaffQueryRequest
{
    public string StaffId { get; set; }
    public Gender? Gender { get; set; }
    public DateTime? FromDate { get; set; }
    public DateTime? ToDate { get; set; }
}