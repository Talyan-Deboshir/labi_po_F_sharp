using System;
using System.Collections.Generic;

namespace Lab
{
    class Program
    {
        static void Main()
        {
            
            DataBase.CreateDefaultDatabase();

            List<Tests> tests = null;
            bool isLoaded = false;

            while (true)
            {
                Console.WriteLine("\n1. Прочитать базу\n2. Просмотреть\n3. Удалить\n4. Добавить\n5. Сохранить\n6. Запросы\n0. Выход");
                int choice = InputValidator.InputIntWithValidation("Ваш выбор:", 0, 6);

                if (choice == 0) break;
                if (choice >= 2 && choice <= 6 && !isLoaded)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Сначала прочитайте базу");
                    Console.ResetColor();
                    continue;
                }


                switch (choice)
                {
                    case 1:
                        tests = DataBase.ReadDatabase();
                        isLoaded = true;
                        Console.WriteLine("БД прочитана.");
                        break;

                    case 2:
                        DataBase.ViewDatabase(tests);
                        break;

                    case 3:
                        Console.WriteLine("1. Название\n2. Профильный\n3. Кол-во заданий\n4. Сложность\n5. Проходной балл");
                        int field = InputValidator.InputIntWithValidation("Поле для удаления:", 1, 5);
                        string value = InputValidator.InputStringWithValidation("Значение поля:");
                        DataBase.DeleteFromDatabase(tests, field, value);
                        Console.WriteLine("Удалено");
                        break;

                    case 4:
                        string subject = InputValidator.InputStringWithValidation("Название предмета:");
                        bool profile = InputValidator.InputBoolWithValidation("Профильный?");
                        uint questionsCount = InputValidator.InputUIntWithValidation("Кол-во заданий:", 1, int.MaxValue);
                        int difficult = InputValidator.InputIntWithValidation("Сложность (0–10):", 0, 10);
                        double passingScore = InputValidator.InputDoubleWithValidation("Проходной балл:");
                        Tests newTest = new Tests(subject, profile, questionsCount, difficult, passingScore);
                        DataBase.AddTest(tests, newTest);
                        Console.WriteLine("Добавлено");
                        break;

                    case 5:
                        DataBase.SaveDatabase(tests);
                        Console.WriteLine("Сохранено");
                        break;

                    case 6:
                        Console.WriteLine("1. По предмету\n2. Профильные\n3. Средний балл\n4. По сложности\n0. Назад");
                        int qch = InputValidator.InputIntWithValidation("Запрос:", 0, 4);
                        switch (qch)
                        {
                            case 0: break;
                            case 1:
                                string s = InputValidator.InputStringWithValidation("Предмет:");
                                List<Tests> bySubject = DataBase.TestsBySubject(tests, s);
                                Console.WriteLine("Результат:");
                                foreach (Tests t in bySubject) Console.WriteLine(t);
                                break;
                            case 2:
                                List<Tests> profiles = DataBase.ProfileTests(tests);
                                Console.WriteLine("Результат:");
                                foreach (Tests t in profiles) Console.WriteLine(t);
                                break;
                            case 3:
                                double average = DataBase.AveragePassingScore(tests);
                                Console.WriteLine($"Средний проходной балл: {average}");
                                break;
                            case 4:
                                int diff = InputValidator.InputIntWithValidation("Сложность (0–10):", 0, 10);
                                int count = DataBase.CountByDifficulty(tests, diff);
                                Console.WriteLine($"Тестов сложности {diff}: {count}");
                                break;
                        }
                        break;
                }
                
            }
        }
    }
}
