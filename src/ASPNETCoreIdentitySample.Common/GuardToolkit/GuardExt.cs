using DNTPersianUtils.Core;

namespace ASPNETCoreIdentitySample.Common.GuardToolkit;

public static class GuardExt
{
    public static bool ContainsNumber(this string inputText)
    {
        return !string.IsNullOrWhiteSpace(inputText) && inputText.ToEnglishNumbers().Any(char.IsDigit);
    }

    public static bool HasConsecutiveChars(this string inputText, int sequenceLength = 3)
    {
        var charEnumerator = StringInfo.GetTextElementEnumerator(inputText);
        var currentElement = string.Empty;
        var count = 1;
        while (charEnumerator.MoveNext())
        {
            if (string.Equals(currentElement, charEnumerator.GetTextElement(), StringComparison.Ordinal))
            {
                if (++count >= sequenceLength)
                {
                    return true;
                }
            }
            else
            {
                count = 1;
                currentElement = charEnumerator.GetTextElement();
            }
        }

        return false;
    }

    public static bool IsEmailAddress(this string inputText)
    {
        return !string.IsNullOrWhiteSpace(inputText) && new EmailAddressAttribute().IsValid(inputText);
    }

    public static bool IsNumeric(this string inputText)
    {
        if (string.IsNullOrWhiteSpace(inputText))
        {
            return false;
        }

        return long.TryParse(inputText.ToEnglishNumbers(), NumberStyles.Number, CultureInfo.InvariantCulture, out _);
    }
}