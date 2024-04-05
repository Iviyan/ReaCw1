namespace ReaCw1.Plants;

using static Utils;

public class Flower : Plant
{
    public string Color { get; set; } = "";

    public override void DisplayInfoToConsole()
    {
        base.DisplayInfoToConsole();
        Console.WriteLine($"Цвет: {Color}");
    }

    public override void ReadDataFromConsole()
    {
        base.ReadDataFromConsole();
        Color = ReadString("Цвет: ");
    } 
}