namespace ReaCw1.Plants;

using static Utils;

public class Tree : Plant
{
    public bool Eatable { get; set; }
    public int Height { get; set; }

    public override void DisplayInfoToConsole()
    {
        base.DisplayInfoToConsole();
        Console.WriteLine($"Съедобное: {Eatable.ToRus()}");
        Console.WriteLine($"Высота: {Height}");
    }

    public override void ReadDataFromConsole()
    {
        base.ReadDataFromConsole();
        Eatable = ReadValue<bool>("Съедобное: ");
        Height = ReadValue<int>("Высота: ");
    } 
}