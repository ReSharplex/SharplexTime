namespace SharplexTimeCode.Core.Models;

public struct ResponseResult
{
    public ResponseStatus IsSuccess { get; }
    public string Error { get; }
    
    public ResponseResult(ResponseStatus isSuccess, string error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static ResponseResult Success()
    {
        return new ResponseResult(ResponseStatus.Success, string.Empty);
    }

    public static ResponseResult Failure(string error)
    {
        return new ResponseResult(ResponseStatus.Failure, error);
    }
    
    public static ResponseResult Warning(string error)
    {
        return new ResponseResult(ResponseStatus.Warning, error);
    }
    
    public static ResponseResult Critical(string error)
    {
        return new ResponseResult(ResponseStatus.Critical, error);
    }
    
    public static ResponseResult PartialSuccess(string error)
    {
        return new ResponseResult(ResponseStatus.PartialSuccess, error);
    }
}