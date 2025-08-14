namespace StaffManagementCore.Exception;

public class ValidatingException : System.Exception
{
    public ValidatingException(string message) : base(message)
    {
    }
}
