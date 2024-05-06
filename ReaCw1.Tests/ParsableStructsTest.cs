using System.Globalization;
using ReaCw1.Types;

namespace ReaCw1.Tests;

public class ParsableStructsTest
{
    [Fact]
    public void GeoParseTest()
    {
        string input = "55 37";
        Geo expected = new(55, 37);
        Assert.True(Geo.TryParse(input, CultureInfo.InvariantCulture, out var geo));
        Assert.Equal(expected, geo);
    }

    [Fact]
    public void DateIntervalParseTest()
    {
        string input = "01.01.2000 02.02.2002";
        DateInterval expected = new(new(2000, 01, 01), new(2002, 02, 02));
        Assert.True(DateInterval.TryParse(input, CultureInfo.InvariantCulture, out var dateInterval));
        Assert.Equal(expected, dateInterval);
    }
}