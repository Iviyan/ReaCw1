using ReaCw1.Monuments;

namespace ReaCw1.Tests;

public class MonumentsProviderTest
{
    [Fact]
    public void CreateDefaultMonumentTest()
    {
        var monument = MonumentsProvider.Create("");
        Assert.NotNull(monument);
        Assert.IsType<Monument>(monument);
    }

    [Fact]
    public void CreateDefaultMonumentExplicitlyTest()
    {
        var monument = MonumentsProvider.Create("Памятник");
        Assert.NotNull(monument);
        Assert.IsType<Monument>(monument);
    }

    [Theory]
    [InlineData("Памятник", "памятник")]
    [InlineData("Надгробие", "надгробие")]
    public void CaseInsensitivityTest(string name1, string name2)
    {
        var monument1 = MonumentsProvider.Create(name1);
        var monument2 = MonumentsProvider.Create(name2);

        Assert.Multiple(() =>
        {
            Assert.NotNull(monument1);
            Assert.NotNull(monument2);
            Assert.Equal(monument1.GetType(), monument2.GetType());
        });
    }

    [Fact]
    public void CreateMonumentByTypeIndexTest()
    {
        int index = 1;
        var monument = MonumentsProvider.Create(index.ToString());
        var expectedType = MonumentsProvider.MonumentTypes[index].Factory().GetType();
        Assert.NotNull(monument);
        Assert.IsType(expectedType, monument);
    }

    [Fact]
    public void CreateUnknownMonumentTest()
    {
        var monument = MonumentsProvider.Create("qwerty");
        Assert.Null(monument);
    }
}