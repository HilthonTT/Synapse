using SharedKernel;

namespace Presentation.Extensions;

public static class ResultExtensions
{
    public static TOut Match<TOut>(
        this Result result, 
        Func<TOut> onSuccess, 
        Func<Result, TOut> onFailure)
    {
        return result.IsSuccess ? onSuccess() : onFailure(result);
    }

    public static TOut Match<TOut, TIn>(
        this Result<TIn> result,
        Func<TIn, TOut> onSuccess,
        Func<Result<TIn>, TOut> onFailure)
    {
        return result.IsSuccess ? onSuccess(result.Value) : onFailure(result);
    }
}
