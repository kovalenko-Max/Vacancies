using GraphQLEngine.Features.Vacancy.Validation.Exceptions;

namespace GraphQLEngine.Features.Vacancy.Validation;
internal class WageValidator
{
    public readonly long? WageFrom;
    public readonly long? WageTo;
    public readonly bool IsExist;

    private WageValidator(long valueFrom, long valueTo)
    {
        WageFrom = valueFrom;
        WageTo = valueTo;
        IsExist = true;
    }

    private WageValidator()
    {
        IsExist = false;
    }

    public static Result<WageValidator, VacancyValidationException> Validate(long? wageFrom, long? wageTo)
    {
        if(wageFrom == null && wageTo == null)
        {
            return new WageValidator();
        }
        else if (wageFrom == null || wageTo == null)
        {
            return new VacancyWageValidationException(nameof(wageFrom), $"Must specify both values Wagefrom and Wageto");
        }
        else if (wageFrom == 0)
        {
            return new VacancyWageValidationException(nameof(wageFrom), $"WageFrom cannot be 0");
        }
        else if (wageTo == 0)
        {
            return new VacancyWageValidationException(nameof(wageTo), $"WageTo cannot be 0");
        }
        else if (wageFrom > wageTo)
        {
            return new VacancyWageValidationException(nameof(wageTo), "WageFrom cannot be bigger than wageTo");
        }

        return new WageValidator(wageFrom.Value, wageTo.Value);
    }
}
