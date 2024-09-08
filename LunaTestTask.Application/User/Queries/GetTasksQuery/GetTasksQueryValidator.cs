using FluentValidation;
using LunaTestTask.Domain.Entities;
using LunaTestTask.Domain.RepositoryParameters;

namespace LunaTestTask.Application.User.Queries.GetTasksQuery;

//validating provided strings to match enums
//if some value is null(not provided by the user) it is ignored
public class GetTasksQueryValidator : AbstractValidator<GetTasksQuery>
{
    public GetTasksQueryValidator()
    {
        RuleFor(x => x.Status)
            .Must(IsValidEnumOrNull<TaskEntityStatus>)
            .WithMessage("Invalid Status value.");

        RuleFor(x => x.DueDate)
            .Custom((dueDate, context) =>
            {
                if (dueDate is not null && !DateTime.TryParse(dueDate, out _))
                {
                    context.AddFailure("Invalid DueDate format.");
                }
            });

        RuleFor(x => x.Priority)
            .Must(IsValidEnumOrNull<TaskEnitityPriority>)
            .WithMessage("Invalid Priority value.");

        RuleFor(x => x.SortBy)
            .Must(IsValidEnumOrNull<SortingOption>)
            .WithMessage("Invalid SortBy value.");

        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Page size must be greater or equal to 0");

        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .WithMessage("Page size must be greater than 0");
    }

    private static bool IsValidEnumOrNull<TEnum>(string? value) where TEnum : struct
    {
        return value is null || Enum.TryParse<TEnum>(value,ignoreCase: true,out _);
    }
}
