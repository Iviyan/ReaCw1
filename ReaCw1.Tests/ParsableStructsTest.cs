using System.Globalization;
using ReaCw1.Types;

namespace ReaCw1.Tests;

public class ParsableStructsTest
{
    [Fact]
    public void GeoParseTest()
    {
        Assert.Multiple(() =>
        {
            Assert.True(Geo.TryParse("55 37", CultureInfo.InvariantCulture, out var geo));
            Assert.Equal(new Geo(55, 37), geo);
        });
    }
    
    [Fact]
    public void DateIntervalParseTest()
    {
        Assert.Multiple(() =>
        {
            Assert.True(DateInterval.TryParse("01.01.2000 02.02.2002", CultureInfo.InvariantCulture, out var dateInterval));
            Assert.Equal(new DateInterval(new(2000, 01, 01), new(2002, 02, 02)), dateInterval);
        });
    }
}