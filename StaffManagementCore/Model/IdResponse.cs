using System.Diagnostics.CodeAnalysis;

namespace StaffManagementCore.Model
{
    [ExcludeFromCodeCoverage]
    public class IdResponse : ResponseBase
    {
        public long Id { get; set; }
    }
}