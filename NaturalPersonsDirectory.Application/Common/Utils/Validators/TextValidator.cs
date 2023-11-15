using NaturalPersonsDirectory.Application.Common.Constants;

namespace NaturalPersonsDirectory.Application.Common.Utils.Validators;

public static class TextValidator
{
    public static bool ContainOnlyGeorgianOrEnglishLetters(string text)
    {
        var containsOnlyGeorgian = text.All(c => c >= UnicodeCharacter.GeorgianFirstLetter && c <= UnicodeCharacter.GeorgianLastLetter);

        var containsOnlyEnglish = text.All(c => (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'));

        return containsOnlyGeorgian || containsOnlyEnglish;
    }

    public static bool ContainOnlyNumbers(string text)
    {
        return text.All(c => char.IsNumber(c));
    }
}
