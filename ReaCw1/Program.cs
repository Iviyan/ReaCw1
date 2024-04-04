using ReaCw1;
using ReaCw1.Monuments;
using static ReaCw1.Utils;

List<Monument> monuments = new();
monuments.Add(new Monument("Памятник камню", "г.Москва, каменная улица", new(2, 2), new(2000, 01, 01), "Большой камень..."));
monuments.Add(new SinglePersonMonument("Памятник А.С. Пушкину", "г.Москва, ул. Пушкина", new(1, 1), new(2000, 01, 01), "",
    "А.С. Пушкин", new(new(1900, 01, 01), new(1950, 01, 01))));

const string commandsInfo = """
                            Команды:
                            l|list - Вывести все памятники.
                            mt|monument types - Вывести типы памятников.
                            n|new - Добавить памятник.
                            r|remove - Удалить памятник.
                            f|find - Поиск.
                            h|help - Справка по командам.
                            q|quit - Выход.
                            """;

Console.WriteLine("Система учета городских памятников.");
Console.WriteLine(commandsInfo);

Console.CancelKeyPress += (sender, e) =>
{
    Save();
    Console.WriteLine("Данные сохранены.");
};

while (true)
{
    Console.Write("> ");
    string command = Console.ReadLine() ?? "";

    if (command is "h" or "help")
    {
        Console.WriteLine(commandsInfo);
    }
    else if (command is "l" or "list")
    {
        foreach (var monument in monuments)
        {
            Console.WriteLine(monument.ToString());
            Console.WriteLine();
        }
    }
    else if (command is "mt" or "monument types")
    {
        foreach (var type in Monument.Factories)
        {
            Console.WriteLine($"'{type.Key}'");
        }

        Console.WriteLine();
    }
    else if (command is "n" or "new")
    {
        Console.WriteLine("Введите тип памятника. Пустая строка - обычный памятник.");
        Monument? monument;
        while (true)
        {
            string monumentType = ReadString("> ");
            monument = Monument.CreateInstanceForType(monumentType);
            if (monument != null) break;
            Console.WriteLine("Введённого типа памятника не существует.");
        }

        var properties = monument.GetProperties();
        foreach (var property in properties)
        {
            if (property.Setter is not { } setter) continue;
            while (true)
            {
                string value = ReadString(property.Name + ": ");

                try
                {
                    setter(value);
                    break;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Неверный формат. " + e.Message);
                }
            }
        }

        monuments.Add(monument);
        Console.WriteLine();
    }
    else if (command is "r" or "remove")
    {
        if (monuments.Count == 0)
        {
            Console.WriteLine("Список памятников пуст");
        }
        else
        {
            while (true)
            {
                int index = ReadValue<int>("Введите номер памятника в списке: ");
                if (index < 0 || index >= monuments.Count)
                {
                    Console.WriteLine($"Номер должен быть от 0 до {monuments.Count}");
                    continue;
                }

                var monument = monuments[index];
                monuments.RemoveAt(index);

                Console.WriteLine($"Памятник \"{monument.Name}\" удалён.");
                break;
            }
        }
    }
    else if (command is "f" or "find")
    {
        string query = ReadString("> ");
        var result = monuments.Where(m => m.Name.Contains(query));
        foreach (var monument in result)
        {
            Console.WriteLine(monument.ToString());
            Console.WriteLine();
        }

        break;
    }
    else if (command is "q" or "quit")
    {
        Save();
        Console.WriteLine("Данные сохранены");
        break;
    }
    else
    {
        Console.WriteLine("Неизвестная команда.");
    }
}

void Save() { }