using System.Diagnostics.CodeAnalysis;
using ReaCw1.Types;

namespace ReaCw1.Monuments;

public class SinglePersonMonument : Monument
{
    [SetsRequiredMembers]
    public SinglePersonMonument() : base() { }

    [SetsRequiredMembers]
    public SinglePersonMonument(string name, string address, Geo geo, DateOnly constructionDate, string description, string personName,
        DateInterval birthAndDeathDates) : base(name, address, geo, constructionDate, description)
    {
        PersonName = personName;
    }

    public required string PersonName { get; set; } = string.Empty;

    public override SortedSet<MonumentPropertyInfo> GetProperties()
    {
        var properties = base.GetProperties();
        properties.Add(new("Тип", () => "Памятник человеку", null, 2000));
        properties.Add(new("Имя человека", () => PersonName, v => PersonName = v, 690));
        return properties;
    }
}