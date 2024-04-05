using System.Collections.Immutable;

namespace ReaCw1.Plants;

using static Utils;

public class Plant
{
    public string Name { get; set; } = "";
    public DateOnly PlacementDate { get; set; }
    public string Description { get; set; } = "";

    public virtual void DisplayInfoToConsole()
    {
        Console.WriteLine($"Название: {Name}");
        Console.WriteLine($"Дата размещения: {PlacementDate:dd.MM.yyyy}");
        Console.WriteLine($"Описание: {Description}");
    }

    public virtual void ReadDataFromConsole()
    {
        Name = ReadValue<string>("Название: ");
        PlacementDate = ReadValue<DateOnly>("Дата размещения: ");
        Description = ReadValue<string>("Описание: ");
    }
}