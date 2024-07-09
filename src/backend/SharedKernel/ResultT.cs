using System.Diagnostics.CodeAnalysis;

namespace SharedKernel;

public class Result<TValue>(TValue? value, bool isSuccess, Error error) 
    : Result(isSuccess, error)
{
    private readonly TValue? _value = value;

    [NotNull]
    public TValue Value => IsSuccess
        ? _value!
        : throw new InvalidOperationException("The value of a failure result can't be accessed.");

    public static implicit operator Result<TValue>(TValue? value) =>
        value is not null ? Success(value) : Failure<TValue>(Error.NullValue);

    public static Result<TValue> ValidationFailure(Error error) =>
        new(default, false, error);
}
