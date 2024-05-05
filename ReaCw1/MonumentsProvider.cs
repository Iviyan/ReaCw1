using ReaCw1.Monuments;

namespace ReaCw1;

public record MonumentFactory(string Name, Func<Monument> Factory);

public static class MonumentsProvider
{
    public static readonly List<MonumentFactory> MonumentTypes = new();

    public static void RegisterType(string name, Func<Monument> factory) =>
        MonumentTypes.Add(new(name, factory));

    public static void RegisterType<TMonument>(string name) where TMonument : Monument, new() =>
        MonumentTypes.Add(new(name, () => new TMonument()));

    /// <param name="name">Name or id</param>
    public static Monument? Create(string name)
    {
        name = name.ToLowerInvariant();

        if (string.IsNullOrWhiteSpace(name)) return MonumentTypes[0].Factory();

        if (int.TryParse(name, out int index) && index >= 0 && index < MonumentTypes.Count)
        {
            return MonumentTypes[index].Factory();
        }

        var factory = MonumentTypes.FirstOrDefault(f => f.Name == name);
        return factory?.Factory() ?? null;
    }

    static MonumentsProvider()
    {
        RegisterType<Monument>("Памятник");
        RegisterType<SinglePersonMonument>("Памятник человеку");
        RegisterType<Tombstone>("Надгробие");
    }
}