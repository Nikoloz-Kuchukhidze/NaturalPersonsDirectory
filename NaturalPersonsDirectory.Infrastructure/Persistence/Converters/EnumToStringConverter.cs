using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace NaturalPersonsDirectory.Infrastructure.Persistence.Converters;

public class EnumToStringConverter<TEnum> : ValueConverter<TEnum, string>
    where TEnum : Enum
{
    public EnumToStringConverter()
        : base(
            enumValue => EnumToString(enumValue),
            stringValue => StringToEnum(stringValue))
    {
    }

    private static string EnumToString(TEnum enumValue)
    {
        return enumValue.ToString();
    }

    private static TEnum StringToEnum(string stringValue)
    {
        if (!Enum.TryParse(typeof(TEnum), stringValue, out var result))
        {
            throw new ArgumentException($"Invalid enum value: {stringValue}");
        }

        return (TEnum)result!;
    }
}
