using ReaCw1.Monuments;

namespace ReaCw1.Tests;

public class MonumentPropertiesTest
{
    [Fact]
    public void PropertiesOrderAndValueTest()
    {
        (string, string)[] expectedProperties =
        [
            ("Тип", "Надгробие"),
            ("Название", "Надгробие кого-то"),
            ("Адрес", "Где-то"),
            ("Координаты", "1, 1"),
            ("Дата возведения", "01.01.2000"),
            ("Имя человека", "Кто-то"),
            ("Годы жизни", "01.01.1900 - 01.01.1950"),
            ("Описание", "..."),
        ];

        Tombstone tombstone = new("Надгробие кого-то", "Где-то", new(1, 1), 
            new(2000, 01, 01), "...", "Кто-то",
            new(new(1900, 01, 01), new(1950, 01, 01)));

        var properties = tombstone.GetProperties();
        var actualProperties = properties.Select(p => (p.Name, p.Getter())).ToArray();
        
        Assert.Equal(expectedProperties, actualProperties);
    }
}