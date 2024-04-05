using System.Globalization;

namespace ReaCw1;

public static class Utils
{
    public static string ReadString(string text)
    {
        Console.Write(text);
        return Console.ReadLine() ?? string.Empty;
    }

    public static T ReadValue<T>(string text) where T : IParsable<T>
    {
        while (true)
        {
            Console.Write(text);
            string input = Console.ReadLine() ?? string.Empty;
            if (T.TryParse(input, CultureInfo.CurrentCulture, out var value)) return value;
            Console.WriteLine("Некорректный ввод.");
        }
    }
}

public static class BoolExtensions
{
    public static string ToRus(this bool value) => value ? "да" : "нет";
}