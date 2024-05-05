using System.Diagnostics.CodeAnalysis;
using ReaCw1.Types;

namespace ReaCw1.Monuments;

public class Monument
{
    [SetsRequiredMembers]
    public Monument() { }

    [SetsRequiredMembers]
    public Monument(string name, string address, Geo geo, DateOnly constructionDate, string description)
    {
        Name = name;
        Address = address;
        Geo = geo;
        ConstructionDate = constructionDate;
        Description = description;
    }

    public required string Name { get; set; } = string.Empty;
    public required string Address { get; set; } = string.Empty;
    public required Geo Geo { get; set; } = default;
    public required DateOnly ConstructionDate { get; set; } = default;
    public required string Description { get; set; } = string.Empty;

    public virtual SortedSet<MonumentPropertyInfo> GetProperties()
    {
        SortedSet<MonumentPropertyInfo> properties = new()
        {
            new("Название", () => Name, v => Name = v, 1000),
            new("Адрес", () => Address, v => Address = v, 900),
            new("Координаты", () => Geo.ToString(), v => Geo.Parse(v), 800),
            new("Дата возведения",
                () => ConstructionDate.ToString("dd.MM.yyyy"),
                v => ConstructionDate = DateOnly.Parse(v),
                700),
            new("Описание", () => Description, v => Description = v, 0),
        };

        if (this.GetType() == typeof(Monument))
        {
            properties.Add(new("Тип", () => "Памятник", v => Name = v, 2000));
        }

        return properties;
    }

    public override string ToString() => string.Join('\n', GetProperties().Select(p => $"{p.Name}: {p.Getter()}"));
}

public readonly record struct MonumentPropertyInfo(string Name, Func<string> Getter, Action<string>? Setter, int Order = 500)
    : IComparable<MonumentPropertyInfo>
{
    public int CompareTo(MonumentPropertyInfo other)
    {
        int orderComparison = -Order.CompareTo(other.Order); // reverse order (max -> min)
        if (orderComparison != 0) return orderComparison;
        return string.Compare(Name, other.Name, StringComparison.Ordinal);
    }
};