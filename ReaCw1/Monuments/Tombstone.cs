using System.Diagnostics.CodeAnalysis;
using ReaCw1.Types;

namespace ReaCw1.Monuments;

class Tombstone : Monument
{
    [SetsRequiredMembers] // Rider error
    static Tombstone() => RegisterType("надгробие", () => new SinglePersonMonument());

    [SetsRequiredMembers]
    public Tombstone() : base() { }

    [SetsRequiredMembers]
    public Tombstone(string name, string address, Geo geo, DateOnly constructionDate, string description, string personName,
        DateInterval birthAndDeathDates) : base(name, address, geo, constructionDate, description)
    {
        PersonName = personName;
        BirthAndDeathDates = birthAndDeathDates;
    }

    public required string PersonName { get; set; } = string.Empty;
    public required DateInterval BirthAndDeathDates { get; set; } = default;

    public override SortedSet<MonumentPropertyInfo> GetProperties()
    {
        var properties = base.GetProperties();
        properties.Add(new("Тип памятника", () => "надгробие", null, 2000));
        properties.Add(new("Имя человека", () => PersonName, v => PersonName = v, 690));
        properties.Add(new("Годы жизни", () => BirthAndDeathDates.ToString(), v => DateInterval.Parse(v), 680));
        return properties;
    }
}