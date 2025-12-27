namespace VirtualPet.Application;

public class Result
{
    public bool IsSuccess { get; }
    public Exception? Error { get; }

    protected Result(bool isSuccess, Exception? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, null);

    public static Result Failure(Exception error) => new(false, error);
}

public class Result<T> : Result
{
    public T? Value { get; }

    private Result(bool isSuccess, T? value, Exception? error)
        : base(isSuccess, error)
    {
        Value = value;
    }

    public static Result<T> Success(T value) => new(true, value, null);

    public static new Result<T> Failure(Exception errorMessage) => new(false, default, errorMessage);
}
