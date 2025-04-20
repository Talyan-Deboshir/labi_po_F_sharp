using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace Lab;
[Serializable]
public struct Toy
{
    private string name;
    private int cost;
    private int minage;
    private int maxage;

    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }
    public int Cost
    {
        get
        {
            return cost;
        }
        set
        {
            if (value < 0)
                throw new ArgumentException("Цена должна быть больше 0");
            cost = value;
        }
    }
    public int MinAge
    {
        get
        {
            return minage;
        }
        set
        {
            if (value < 0 || value > 18)
                throw new ArgumentException("Возраст от 0 до 18");
            minage = value;
        }
    }
    public int MaxAge
    {
        get
        {
            return maxage;
        }
        set
        {
            if (value < 0 || value > 18)
                throw new ArgumentException("Возраст от 0 до 18");
            maxage = value;
        }
    }

    public Toy()
    {

    }

    public Toy(string name, int cost, int minAge, int maxAge)
    {
        Name = name;
        Cost = cost;
        MinAge = minAge;
        MaxAge = maxAge;
    }
}


public static class Tasks
{
    // 1.1 Генерация файла с целыми числами
    public static void GenerateFileForTask1(string filename, int count, int minValue, int maxValue)
    {
        Random rand = new Random();
        StreamWriter writer = new StreamWriter(filename);
        try
        {
            for (int i = 0; i < count; i++)
            {
                int number = rand.Next(minValue, maxValue + 1);
                writer.WriteLine(number);
            }
        }
        finally
        {
            writer.Close();
        }
    }

    // 1.2 Уменьшение каждого элемента на 1
    public static void ProcessFileForTask1(string inputFilename, string outputFilename)
    {
        StreamReader reader = new StreamReader(inputFilename);
        StreamWriter writer = new StreamWriter(outputFilename);
        try
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (int.TryParse(line, out int number))
                {
                    writer.WriteLine(number - 1);
                }
            }
        }
        finally
        {
            reader.Close();
            writer.Close();
        }
    }

    // 2.1 Генерация файла с несколькими числами в строке
    public static void GenerateFileForTask2(string filename, int lineCount, int numbersPerLine, int minValue, int maxValue)
    {
        Random rand = new Random();
        StreamWriter writer = new StreamWriter(filename);
        try
        {
            for (int i = 0; i < lineCount; i++)
            {
                for (int j = 0; j < numbersPerLine; j++)
                {
                    int number = rand.Next(minValue, maxValue + 1);
                    writer.Write(number + " ");
                }
                writer.WriteLine();
            }
        }
        finally
        {
            writer.Close();
        }
    }

    // 2.2 Нахождение разности первого и максимального элементов
    public static int FindDifferenceForTask2(string filename)
    {
        int first = 0;
        int max = 0;
        bool firstFound = false;

        StreamReader reader = new StreamReader(filename);
        try
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                for (int i = 0; i < parts.Length; i++)
                {
                    if (int.TryParse(parts[i], out int number))
                    {
                        if (!firstFound)
                        {
                            first = number;
                            max = number;
                            firstFound = true;
                        }
                        else if (number > max)
                        {
                            max = number;
                        }
                    }
                }
            }
        }
        finally
        {
            reader.Close();
        }

        if (firstFound)
        {
            return first - max;
        }
        else
        {
            throw new InvalidOperationException("Файл пуст или не содержит целых чисел.");
        }
    }

    // 3. Копирование строк, начинающихся с буквы 'б'
    public static void ProcessFileForTask3(string inputFilename, string outputFilename)
    {
        StreamReader reader = new StreamReader(inputFilename, Encoding.UTF8);
        StreamWriter writer = new StreamWriter(outputFilename, false, Encoding.UTF8);
        try
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                if (line.TrimStart().StartsWith("б", StringComparison.Ordinal))
                {
                    writer.WriteLine(line);
                }
            }
        }
        finally
        {
            reader.Close();
            writer.Close();
        }
    }

    // 4.1 Генерация бинарного файла с числами
    public static void GenerateFileForTask4(string filename, int count, int minValue, int maxValue)
    {
        Random rand = new Random();
        BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.Create));
        try
        {
            for (int i = 0; i < count; i++)
            {
                int number = rand.Next(minValue, maxValue + 1);
                writer.Write(number);
            }
        }
        finally
        {
            writer.Close();
        }

     // 4.2 Чтение и вывод содержимого файла
        BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open));
        try
        {
            Console.WriteLine("Содержимое файла после генерации: ");
            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                int number = reader.ReadInt32();
                Console.WriteLine(number);
            }
        }
        finally
        {
            reader.Close();
        }
    }

    // 4.3 Нахождение разности первого и максимального элементов
    public static int FindDifferenceForTask4(string filename)
    {
        BinaryReader reader = new BinaryReader(File.Open(filename, FileMode.Open));
        try
        {
            if (reader.BaseStream.Length == 0)
            {
                throw new InvalidOperationException("Файл пуст.");
            }

            int first = reader.ReadInt32();
            int max = first;

            while (reader.BaseStream.Position < reader.BaseStream.Length)
            {
                int number = reader.ReadInt32();
                if (number > max)
                {
                    max = number;
                }
            }

            return first - max;
        }
        finally
        {
            reader.Close();
        }
    }
    // 5.1 Генерация файла с данными об игрушках с XML-сериализацией
    public static void GenerateFileForTask5(string filename, List<Toy> toys)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Toy>));
        StreamWriter writer = new StreamWriter(filename);
        try
        {
            serializer.Serialize(writer, toys);
        }
        finally
        {
            writer.Close();
        }
    }

    // 5.2 Подсчет общей стоимости кукол для детей 6ти лет
    public static int CalculateCostForTask5(string filename)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Toy>));
        StreamReader reader = new StreamReader(filename);
        try
        {
            List<Toy> toys = (List<Toy>)serializer.Deserialize(reader);
            int sum = 0;
            for (int i = 0; i < toys.Count; i++)
            {
                if (toys[i].Name.ToLower().Contains("кукла") && toys[i].MinAge <= 6 && toys[i].MaxAge >= 6)
                {
                    sum += toys[i].Cost;
                }
            }
            return sum;
        }
        finally
        {
            reader.Close();
        }
    }

    // 6. Удаление дубликатов из списка, кроме первого вхождения
    public static List<string> ProcessListForTask6(List<string> list)
    {
        List<string> result = new List<string>();
        for (int i = 0; i < list.Count; i++)
        {
            if (!result.Contains(list[i]))
            {
                result.Add(list[i]);
            }
        }
        return result;
    }

    // 7. Обработка двусвязного списка с заменой неравных соседей элемента "E"
    public static void ProcessLinkedListForTask7(LinkedList<string> list)
    {
        LinkedListNode<string> current = list.First;
        while (current != null)
        {
            if (current.Value == "E" && current.Previous != null && current.Next != null)
            {
                if (current.Previous.Value != current.Next.Value)
                {
                    string temp = current.Previous.Value;
                    current.Previous.Value = current.Next.Value;
                    current.Next.Value = temp;
                }
            }
            current = current.Next;
        }
    }

    // 8. Анализ знания языков сотрудниками
    public static void ProcessLanguagesForTask8()
    {
        string[] languages = { "English", "French", "German", "Spanish", "Italian", "Russian" };
        int employeeCount = InputValidator.InputIntWithValidation("Введите количество сотрудников:", 1, int.MaxValue);
        List<HashSet<string>> employees = new List<HashSet<string>>();

        for (int i = 0; i < employeeCount; i++)
        {
            Console.WriteLine($"Сотрудник {i + 1}:");
            HashSet<string> employeeLanguages = new HashSet<string>();
            bool done = false;
            while (!done)
            {
                Console.WriteLine("Выберите язык (введите номер, 0 для завершения):");
                for (int j = 0; j < languages.Length; j++)
                {
                    Console.WriteLine($"{j + 1}. {languages[j]}");
                }
                int num = InputValidator.InputIntWithValidation("Номер:", 0, languages.Length);
                if (num == 0)
                {
                    done = true;
                }
                else
                {
                    employeeLanguages.Add(languages[num - 1]);
                }
            }
            employees.Add(employeeLanguages);
        }

        HashSet<string> knownByAll = new HashSet<string>(employees[0]);
        for (int i = 1; i < employees.Count; i++)
        {
            knownByAll.IntersectWith(employees[i]);
        }

        HashSet<string> knownByOne = new HashSet<string>();
        for (int i = 0; i < employees.Count; i++)
        {
            knownByOne.UnionWith(employees[i]);
        }

        HashSet<string> knownByNone = new HashSet<string>(languages);
        knownByNone.ExceptWith(knownByOne);

        Console.WriteLine("Языки, которые знают все сотрудники:");
        foreach (string lang in knownByAll)
        {
            Console.WriteLine(lang);
        }

        Console.WriteLine("Языки, которые знает хотя бы один сотрудник:");
        foreach (string lang in knownByOne)
        {
            Console.WriteLine(lang);
        }

        Console.WriteLine("Языки, которые не знает никто из сотрудников:");
        foreach (string lang in knownByNone)
        {
            Console.WriteLine(lang);
        }
    }

    // 9. Определение начальных букв слов в файле
    public static HashSet<char> ProcessFileForTask9(string filename)
    {
        HashSet<char> firstLetters = new HashSet<char>();
        StreamReader reader = new StreamReader(filename, Encoding.UTF8);
        try
        {
            string text = reader.ReadToEnd();
            string word = "";
            for (int i = 0; i < text.Length; i++)
            {
                if (char.IsLetter(text[i]))
                {
                    word += text[i];
                }
                else
                {
                    if (word.Length > 0)
                    {
                        char firstLetter = char.ToLower(word[0]);
                        firstLetters.Add(firstLetter);
                        word = "";
                    }
                }
            }
            if (word.Length > 0)
            {
                char firstLetter = char.ToLower(word[0]);
                firstLetters.Add(firstLetter);
            }
        }
        finally
        {
            reader.Close();
        }
        return firstLetters;
    }
}

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Выберите задание (1-9). 0 Для выхода");
            int choice = InputValidator.InputIntWithValidation("Выбор: ", 0, 9);
            if (choice == 0) break;

            switch (choice)
            {
                case 1:
                    string task1Input = "task1_input.txt";
                    string task1Output = "task1_output.txt";
                    int count1 = InputValidator.InputIntWithValidation("Введите количество чисел:", 1, 1000);
                    int minValue1 = InputValidator.InputIntWithValidation("Введите минимальное значение:", int.MinValue, int.MaxValue);
                    int maxValue1 = InputValidator.InputIntWithValidation("Введите максимальное значение:", minValue1, int.MaxValue);
                    Tasks.GenerateFileForTask1(task1Input, count1, minValue1, maxValue1);
                    Tasks.ProcessFileForTask1(task1Input, task1Output);
                    Console.WriteLine($"Файл {task1Output} создан.");
                    break;

                case 2:
                    string task2Input = "task2_input.txt";
                    int lineCount = InputValidator.InputIntWithValidation("Введите количество строк:", 1, 100);
                    int numbersPerLine = InputValidator.InputIntWithValidation("Введите количество чисел в строке:", 1, 100);
                    int minValue2 = InputValidator.InputIntWithValidation("Введите минимальное значение:", int.MinValue, int.MaxValue);
                    int maxValue2 = InputValidator.InputIntWithValidation("Введите максимальное значение:", minValue2, int.MaxValue);
                    Tasks.GenerateFileForTask2(task2Input, lineCount, numbersPerLine, minValue2, maxValue2);
                    int difference2 = Tasks.FindDifferenceForTask2(task2Input);
                    Console.WriteLine($"Разность первого и максимального элементов: {difference2}");
                    break;

                case 3:
                    string task3Input = "task3_input.txt";
                    string task3Output = "task3_output.txt";
                    Tasks.ProcessFileForTask3(task3Input, task3Output);
                    Console.WriteLine($"Файл {task3Output} создан.");
                    break;

                case 4:
                    string task4Input = "task4_input.bin";
                    int count4 = InputValidator.InputIntWithValidation("Введите количество чисел:", 1, 1000);
                    int minValue4 = InputValidator.InputIntWithValidation("Введите минимальное значение:", int.MinValue, int.MaxValue);
                    int maxValue4 = InputValidator.InputIntWithValidation("Введите максимальное значение:", minValue4, int.MaxValue);
                    Tasks.GenerateFileForTask4(task4Input, count4, minValue4, maxValue4);
                    int difference4 = Tasks.FindDifferenceForTask4(task4Input);
                    Console.WriteLine($"Разность первого и максимального элементов: {difference4}");
                    break;

                case 5:
                    List<Toy> toys = new List<Toy> {
            new Toy("Кукла Вуди", 500, 2, 7),
            new Toy("Базз Лайтер", 5000, 7, 12),
            new Toy("Шрек", 1500, 3, 10),
            new Toy("Кукла Хагги Вагги", 300, 10, 14),
            new Toy("Наруто", 200, 6, 9),
            new Toy("Кукла Барби", 3000, 3, 5),
            new Toy("Крош", 700, 2, 7),
            new Toy("Кукла Ребенка", 2000, 4, 10),
          };
                    string task5Input = "task5_input.xml";
                    Tasks.GenerateFileForTask5(task5Input, toys);
                    int dollCost = Tasks.CalculateCostForTask5(task5Input);
                    Console.WriteLine($"Сумма стоимости кукол для детей 6 лет: {dollCost}");
                    break;

                case 6:
                    int count6 = InputValidator.InputIntWithValidation("Введите количество элементов:", 0, int.MaxValue);
                    List<string> list6 = new List<string>();
                    for (int i = 0; i < count6; i++)
                    {
                        string item = InputValidator.InputStringWithValidation($"Элемент {i + 1}:");
                        list6.Add(item);
                    }
                    List<string> outputList = Tasks.ProcessListForTask6(list6);
                    Console.WriteLine("Список без дубликатов:");
                    for (int i = 0; i < outputList.Count; i++)
                    {
                        Console.WriteLine(outputList[i]);
                    }
                    break;

                case 7:
                    int count7 = InputValidator.InputIntWithValidation("Введите количество элементов:", 0, int.MaxValue);
                    LinkedList<string> list7 = new LinkedList<string>();
                    for (int i = 0; i < count7; i++)
                    {
                        string item = InputValidator.InputStringWithValidation($"Элемент {i + 1}:");
                        list7.AddLast(item);
                    }
                    Tasks.ProcessLinkedListForTask7(list7);
                    Console.WriteLine("Обработанный связный список:");
                    foreach (string item in list7)
                    {
                        Console.WriteLine(item);
                    }
                    break;

                case 8:
                    Tasks.ProcessLanguagesForTask8();
                    break;

                case 9:
                    string task9Input = "task9_input.txt";
                    HashSet<char> firstLetters = Tasks.ProcessFileForTask9(task9Input);
                    Console.WriteLine("Буквы, с которых начинаются слова:");
                    foreach (char letter in firstLetters)
                    {
                        Console.Write(letter + " ");
                    }
                    Console.WriteLine();
                    break;
            }
        }
    }
}