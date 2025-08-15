using StaffManagementCore.Exception;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace StaffManagementCore.Model;

[ExcludeFromCodeCoverage]
public class ResponseBase
{
    [JsonPropertyName("responseCode")]
    public ResponseCodeEnum ResponseCode { get; set; }

    [JsonPropertyName("responseMessage")]
    public string ResponseMessage { get; set; } = "Operation Successful";

    public ResponseBase()
    {
    }

    public ResponseBase(ResponseCodeEnum responseCode, string responseMessage)
    {
        ResponseCode = responseCode;
        ResponseMessage = responseMessage;
    }

    public void EnsureSuccessStatusCode()
    {
        if (ResponseCode != ResponseCodeEnum.Ok) throw new ValidatingException(ResponseMessage);
    }

    public void CopyBaseTo(ResponseBase response)
    {
        response.ResponseCode = ResponseCode;
        response.ResponseMessage = ResponseMessage;
    }
}

public enum ResponseCodeEnum
{
    Ok = 0,
    Error = 1,
}