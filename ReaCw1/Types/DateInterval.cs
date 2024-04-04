namespace ReaCw1.Types;

public readonly record struct DateInterval(DateOnly From, DateOnly To) : IParsable<DateInterval>
{
    private static readonly char[] Separators = { ' ', '-' };
    public static DateInterval Parse(string s, IFormatProvider? provider = null) =>
        TryParse(s, provider, out var dateInterval) ? dateInterval : throw new FormatException();

    public static bool TryParse(string? s, IFormatProvider? provider, out DateInterval result)
    {
        if (s?.Split(Separators, 2, StringSplitOptions.RemoveEmptyEntries) is [ var f, var t ] &&
            DateOnly.TryParse(f, out var from) && DateOnly.TryParse(t, out var to))
        {
            result = new(from, to);
            return true;
        }

        result = default;
        return false;
    }

    public override string ToString() => $"{From:dd.MM.yyyy} - {To:dd.MM.yyyy}";
}