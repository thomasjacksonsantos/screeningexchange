namespace ScreeningExchange.Infrastructure.Core;

public class Result
{
    public bool IsSuccess { get; set; }
    public string? Message { get; set; }
    public ICollection<ErrorValidation>? Errors { get; set; }

    public static Result RequestError(
        string message,
        ICollection<ErrorValidation>? errors = null
    )
        => new Result
        {
            Message = message,
            Errors = errors,
            IsSuccess = false
        };

    public static Result<T> RequestError<T>(
        string message,
        ICollection<ErrorValidation>? errors = null
    )
        => new Result<T>
        {
            Message = message,
            IsSuccess = false,
            Errors = errors
        };

    public static Result Fail(
        string message
    )
    => new Result
    {
        Message = message,
        IsSuccess = false
    };

    public static Result<T> Fail<T>(
        string message
    )
    => new Result<T>
    {
        Message = message,
        IsSuccess = false
    };

    public static Result Ok(
        string message
    )
    => new Result
    {
        Message = message,
        IsSuccess = true
    };    

    public static Result<T> Ok<T>(
        T data,
        string? message = ""
    )
    => new Result<T>
    {
        Message = message,
        IsSuccess = true,
        Data = data
    };

    public static Result<T> Ok<T>(
    )
    => new Result<T>
    {
        IsSuccess = true
    };
}

public class Result<T> : Result
{
    public T? Data { get; set; }
}
