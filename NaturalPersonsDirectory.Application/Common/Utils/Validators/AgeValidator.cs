namespace NaturalPersonsDirectory.Application.Common.Utils.Validators;

public static class AgeValidator
{
    public static bool BeOlderThanEighteen(DateOnly birthDate, DateTimeOffset currentDate)
    {
        return currentDate.Year - birthDate.Year >= 18 ||
            currentDate.Year - birthDate.Year >= 17 &&
            (currentDate.Month < birthDate.Month || (currentDate.Month == birthDate.Month && currentDate.Day < birthDate.Day));
    }
}
