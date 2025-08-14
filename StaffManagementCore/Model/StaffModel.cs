using StaffManagementCore.Exception;

namespace StaffManagementCore.Model;

public class StaffModel : ModelBase
{
    public string StaffId { get; set; }

    public string FullName { get; set; }

    public DateTime? Birthday { get; set; }
    public Gender Gender { get; set; } = Gender.Male;

    public void EnsureValidForAdd()
    {
        if (Enum.IsDefined(Gender) == false) throw new ValidatingException("Invalid Gender");
        Valid(ValidStaffId());
        Valid(ValidFullName());
    }

    public void EnsureValidForUpdate()
    {
        EnsureValidForAdd();
    }

    private static void Valid(string content)
    {
        if (string.IsNullOrEmpty(content) == false) throw new ValidatingException(content);
    }

    public string ValidFullName()
    {
        var length = (FullName + string.Empty).Trim().Length;
        if (length == 0) return "Full Name is required";
        else if (length > 100) return "Full Name is limited 100 characters";
        return string.Empty;
    }

    public string ValidStaffId()
    {
        var length = (StaffId + string.Empty).Trim().Length;
        if (length == 0) return "Staff Id is required";
        else if (length > 10) return "Staff Id is limited 10 characters";
        return string.Empty;
    }
}

public enum Gender
{
    Male = 1,
    Female = 2
}