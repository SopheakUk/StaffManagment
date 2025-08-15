using Microsoft.AspNetCore.Mvc;
using System.Diagnostics.CodeAnalysis;

namespace StaffManagementAPI.Api;

[Route("")]
[ApiController]
[ExcludeFromCodeCoverage]
public class HomeController() : ControllerBase
{
    [HttpGet("")]
    public string Hello() => "Staff Management API";
}