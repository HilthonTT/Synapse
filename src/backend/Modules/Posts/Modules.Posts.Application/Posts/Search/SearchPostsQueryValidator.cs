using FluentValidation;

namespace Modules.Posts.Application.Posts.Search;

internal sealed class SearchPostsQueryValidator : AbstractValidator<SearchPostsQuery>
{
    public SearchPostsQueryValidator()
    {
        RuleFor(query => query.SortColumn)
            .Must(BeAValidEnumValue)
            .WithErrorCode(PostErrorCodes.SearchPosts.IncorrectSortColumn)
            .WithMessage(query => $"{query.SortColumn} is not a valid SortColumn.");

        RuleFor(query => query.SortOrder)
            .Must(BeAValidEnumValue)
            .WithErrorCode(PostErrorCodes.SearchPosts.IncorrectSortOrder)
            .WithMessage(query => $"{query.SortOrder} is not a valid SortOrder.");
    }

    private static bool BeAValidEnumValue<T>(T value) 
        where T : struct, Enum
    {
        return Enum.IsDefined(typeof(T), value);
    }
}
