using ReaCw1;
using ReaCw1.Plants;
using static ReaCw1.Utils;

Greenhouse greenhouse;

const string help = """
                    h - Справка по командам.
                    i - Общая информация.
                    f <текст?> - Поиск.
                    n - Добавить растение.
                    r <место> - Убрать растение.
                    q - Выйти.
                    """;

Console.WriteLine("Система учёта растений в оранжерее\n");

for (;;)
{
    int count = ReadValue<int>("Введите количество мест в оранжерее: ");
    if (count > 0)
    {
        greenhouse = new(count);
        break;
    }

    Console.WriteLine("Количество мест должно быть больше нуля.");
}

Console.WriteLine();
Console.WriteLine("Комманды:");
Console.WriteLine(help);
Console.WriteLine();

for (bool end = false; end == false;)
{
    string command = ReadString("> ");

    switch (command)
    {
        case "h":
        {
            Console.WriteLine(help);
            break;
        }
        case "i":
        {
            Console.WriteLine($"Всего мест: {greenhouse.Capacity}");
            Console.WriteLine($"Свободно мест: {greenhouse.Available}");
            Console.WriteLine($"Занято мест: {greenhouse.Used}");
            break;
        }
        case var _ when command.StartsWith('f') && (command.Length == 1 || command[1] == ' '):
        {
            string query = command is [ 'f', ' ', .. { } s ] ? s : "";
            var result = greenhouse.Where(p => p.Plant.Name.Contains(query));
            foreach (var place in result)
            {
                Console.WriteLine($"Расположение: {place.Place}");
                place.Plant.DisplayInfoToConsole();
                Console.WriteLine();
            }

            break;
        }
        case "n":
        {
            if (greenhouse.Available == 0)
            {
                Console.WriteLine("В оранжерее нет свободных мест.");
                break;
            }

            int place;
            for (;;)
            {
                place = ReadValue<int>("Введите номер места: ");
                if (place < 0 || place >= greenhouse.Capacity)
                {
                    Console.WriteLine($"Номер места может быть от 0 до {greenhouse.Capacity - 1}.");
                }
                else if (greenhouse[place] != null)
                {
                    Console.WriteLine("Место уже занято.");
                }
                else break;
            }

            Console.WriteLine("Типы растений:");
            for (int i = 0; i < PlantsContainer.PlantTypes.Count; i++)
            {
                Console.WriteLine($"{i}. {PlantsContainer.PlantTypes.GetKeyAtIndex(i)}");
            }
            Console.WriteLine();

            Plant? plant = null;
            for (;;)
            {
                string type = ReadString("Введите тип растения: ");
                if (int.TryParse(type, out var typeIndex) && typeIndex >= 0 && typeIndex < PlantsContainer.PlantTypes.Count)
                {
                    type = PlantsContainer.PlantTypes.GetKeyAtIndex(typeIndex);
                }

                plant = PlantsContainer.Create(type);
                if (plant != null) break;
                Console.WriteLine("Неизвестный тип растения.");
            }

            plant.ReadDataFromConsole();
            greenhouse[place] = plant;

            break;
        }
        case [ 'r', ' ', .. { } s ] when int.TryParse(s, out var place):
        {
            if (place < 0 || place >= greenhouse.Capacity)
            {
                Console.WriteLine($"Номер места может быть от 0 до {greenhouse.Capacity - 1}.");
                break;
            }

            if (greenhouse[place] == null)
            {
                Console.WriteLine("Место пустое.");
                break;
            }

            var plant = greenhouse[place]!;
            greenhouse[place] = null;
            Console.WriteLine($"Растение {plant.Name} убрано.");

            break;
        }
        case "q":
        {
            end = true;
            break;
        }
        default:
        {
            Console.WriteLine("Неизвестная команда.");
            break;
        }
    }

    Console.WriteLine();
}