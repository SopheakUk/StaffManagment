using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using StaffManagementCore.Model;
using System.Diagnostics.CodeAnalysis;
using System.Net;

namespace StaffManagementAPI.Filter;

[ExcludeFromCodeCoverage]
public class ExceptionFilter : ActionFilterAttribute
{
    public override void OnActionExecuted(ActionExecutedContext context)
    {
        if (context.Exception is not null)
        {
            var message = context.Exception.Message;
            context.Result = new ObjectResult(new ResponseBase(ResponseCodeEnum.Error, message)) { StatusCode = (int)HttpStatusCode.OK };
            context.ExceptionHandled = true;
        }
    }
}