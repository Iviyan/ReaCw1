using ReaCw1;
using ReaCw1.Monuments;
using static ReaCw1.Utils;

List<Monument> monuments = new();

const string commandsInfo = """
                            Команды:
                            l|list - Вывести все памятники.
                            f|find - Поиск.
                            mt|monument types - Вывести типы памятников.
                            n|new - Добавить памятник.
                            r|remove - Удалить памятник.
                            h|help - Справка по командам.
                            q|quit - Выход.
                            """;

Console.WriteLine("Система учета городских памятников.");
Console.WriteLine(commandsInfo);
Console.WriteLine();

while (true)
{
    string command = ReadString("> ").ToLowerInvariant();
    var commandParts = command.Split(' ', 2);
    string commandName = commandParts.ElementAtOrDefault(0) ?? "";
    string commandArg = commandParts.ElementAtOrDefault(1) ?? "";

    if (commandName is "h" or "help")
    {
        Console.WriteLine(commandsInfo);
    }
    else if (commandName is "l" or "list" or "f" or "find")
    {
        var query = commandArg.ToLowerInvariant();

        var result = monuments
            .Select((m, i) => (index: i, monument: m))
            .Where(m => m.monument.Name.ToLowerInvariant().Contains(query));

        foreach (var monument in result)
        {
            Console.WriteLine($"Номер: {monument.index}");
            Console.WriteLine(monument.monument.ToString());
            Console.WriteLine();
        }
    }
    else if (commandName is "mt" or "monument types")
    {
        for (int i = 0; i < MonumentsProvider.MonumentTypes.Count; i++)
        {
            var type = MonumentsProvider.MonumentTypes[i];
            Console.WriteLine($"{i}) '{type.Name}'");
        }

        Console.WriteLine();
    }
    else if (commandName is "n" or "new")
    {
        Console.WriteLine("Введите тип (или его номер) памятника. Пустая строка - обычный памятник.");
        for (int i = 0; i < MonumentsProvider.MonumentTypes.Count; i++)
        {
            var type = MonumentsProvider.MonumentTypes[i];
            Console.WriteLine($"{i}) '{type.Name}'");
        }

        Monument? monument;
        while (true)
        {
            string monumentType = ReadString("> ");
            monument = MonumentsProvider.Create(monumentType);
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
    }
    else if (commandName is "r" or "remove")
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
    else if (commandName is "q" or "quit")
    {
        break;
    }
    else
    {
        Console.WriteLine("Неизвестная команда.");
    }

    Console.WriteLine();
}