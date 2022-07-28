namespace GraphQLEngine;

public interface IResult { }

public class Result<TValue, TError> : IResult
    where TValue : class
    where TError : class
{
    private readonly TValue? _value;
    public readonly TError[] Errors;

    protected Result(TValue? value, TError[] errors)
    {
        _value = value;
        Errors = errors;
    }

    public bool IsSucceeded => Errors.Empty();
    public bool IsFailed => !IsSucceeded;

    public TValue GetValueForSure()
    {
        if (IsFailed)
        {
            throw new InvalidOperationException();
        }
        return _value!;
    }
    public TValue? GetValue() => _value;

    public static Result<TValue, TError> Succeeded(TValue value)
    {
        return new Result<TValue, TError>(value, errors: Array.Empty<TError>());
    }

    public new static Result<TValue, TError> Failed(params TError[] errors)
    {
        return new Result<TValue, TError>(value: default, errors);
    }

    public static implicit operator Result<TValue, TError>(TValue data) => Succeeded(data);
    public static implicit operator Result<TValue, TError>(TError[] errors) => Failed(errors);
    public static implicit operator Result<TValue, TError>(TError error) => Failed(error);

    public static implicit operator Result<TError>(Result<TValue, TError> result) => result.Errors;
}


public class Result<TError> : Result<object, TError>, IResult
    where TError : class
{
    private Result(TError[] errors) : base(value: null, errors)
    {
    }

    public static Result<TError> Succeeded()
    {
        return new Result<TError>(errors: Array.Empty<TError>());
    }

    public static Result<TError> Failed(params TError[] errors)
    {
        return new Result<TError>(errors);
    }

    public static implicit operator Result<TError>(TError[] errors) => Failed(errors);
    public static implicit operator Result<TError>(TError error) => Failed(error);
}