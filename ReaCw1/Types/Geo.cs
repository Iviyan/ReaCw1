namespace ReaCw1.Types;

public readonly record struct Geo(double Latitude, double Longitude) : IParsable<Geo>
{
    public static Geo Parse(string s, IFormatProvider? provider = null) =>
        TryParse(s, provider, out var geo) ? geo : throw new FormatException();

    public static bool TryParse(string? s, IFormatProvider? provider, out Geo result)
    {
        if (s?.Split(' ', 2) is [ var lat, var lon ] &&
            double.TryParse(lat, out var latitude) && double.TryParse(lon, out var longitude))
        {
            result = new(latitude, longitude);
            return true;
        }

        result = default;
        return false;
    }

    public override string ToString() => $"{Latitude}, {Longitude}";
};