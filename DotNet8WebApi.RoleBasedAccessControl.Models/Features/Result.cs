namespace DotNet8WebApi.RoleBasedAccessControl.Models.Features;

public class Result<T>
{
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public T Data { get; set; }

    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string Token { get; set; }
    public string Message { get; set; }
    public bool IsSuccess { get; set; }
    public bool IsError
    {
        get { return !IsSuccess; }
    }
    public EnumStatusCode StatusCode { get; set; }

    public static Result<T> SuccessResult(
        string message = "Success.",
        EnumStatusCode statusCode = EnumStatusCode.Success
    )
    {
        return new Result<T>
        {
            Message = message,
            IsSuccess = true,
            StatusCode = statusCode
        };
    }

    public static Result<T> SuccessResult(
        T data,
        string message = "Success.",
        EnumStatusCode statusCode = EnumStatusCode.Success
    )
    {
        return new Result<T>
        {
            Message = message,
            IsSuccess = true,
            StatusCode = statusCode,
            Data = data
        };
    }

    public static Result<T> FailureResult(
        string message = "Fail.",
        EnumStatusCode statusCode = EnumStatusCode.BadRequest
    )
    {
        return new Result<T>
        {
            Message = message,
            IsSuccess = false,
            StatusCode = statusCode
        };
    }

    public static Result<T> FailureResult(Exception ex)
    {
        return new Result<T>
        {
            Message = ex.ToString(),
            IsSuccess = false,
            StatusCode = EnumStatusCode.InternalServerError
        };
    }

    public static Result<T> ExecuteResult(
        int result,
        EnumStatusCode successStatusCode = EnumStatusCode.Success,
        EnumStatusCode failureStatusCode = EnumStatusCode.BadRequest
    )
    {
        return result > 0
            ? Result<T>.SuccessResult(statusCode: successStatusCode)
            : Result<T>.FailureResult(statusCode: failureStatusCode);
    }
}
