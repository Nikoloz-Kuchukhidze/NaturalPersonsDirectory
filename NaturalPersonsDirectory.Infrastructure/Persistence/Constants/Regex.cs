namespace NaturalPersonsDirectory.Infrastructure.Persistence.Constants;

public static class Regex
{
    public const string GeorgianAndEnglishLetters = @"^[a-zA-Z ა-ჰ]+$";
    public const string GeorgianOrEnglishLetters = @"^[\p{IsGeorgian}]+$|^[\p{IsBasicLatin}\p{IsLatin-1Supplement}\p{IsLatinExtended-A}\p{IsLatinExtended-B}\p{IsLatinExtendedAdditional}]+$";
}
