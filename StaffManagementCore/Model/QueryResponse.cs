using System.Diagnostics.CodeAnalysis;

namespace StaffManagementCore.Model;

[ExcludeFromCodeCoverage]
public class QueryResponse<T> : ResponseBase
{
    public List<T> Results { get; set; }
}