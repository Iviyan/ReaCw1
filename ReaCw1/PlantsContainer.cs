using ReaCw1.Plants;

namespace ReaCw1;

public static class PlantsContainer
{
    public static readonly SortedList<string, Func<Plant>> PlantTypes = new();

    public static void RegisterType(string name, Func<Plant> factory) => PlantTypes.Add(name, factory);

    public static void RegisterType<TPlant>(string name) where TPlant : Plant, new() => PlantTypes.Add(name, () => new TPlant());

    public static Plant? Create(string name) => PlantTypes.TryGetValue(name, out var factory) ? factory() : null;
    
    static PlantsContainer()
    {
        RegisterType<Plant>("Растение");
        RegisterType<Tree>("Дерево");
        RegisterType<Flower>("Цветок");
    }
}