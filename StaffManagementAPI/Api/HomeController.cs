using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StaffManagementAPI.Api;

[Route("")]
[ApiController]
public class HomeController() : ControllerBase
{
    [HttpGet("")]
    public string Hello() => "Staff Management API";
}
