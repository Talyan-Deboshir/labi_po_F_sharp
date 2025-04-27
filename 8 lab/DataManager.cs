using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Lab
{
    public static class DataBase
    {
        private const string FileName = "database.bin";

        public static void CreateDefaultDatabase()
        {
            List<Tests> tests = new List<Tests>
            {
                new Tests("Математика",   true,  20, 8,  75.5),
                new Tests("Математика",   false,  13, 3,  40.0),
                new Tests("Физика",      true, 44, 6,  60.0),
                new Tests("Физика",      false, 21, 4,  45.5),
                new Tests("Химия",       false, 30, 5,  55.0),
                new Tests("Биология",    false,  23, 7,  65.0),
                new Tests("Информатика", true,  25, 5,  55.5)
            };
            SaveDatabase(tests);
        }

        public static List<Tests> ReadDatabase()
        {
            List<Tests> tests = new List<Tests>();
            BinaryReader reader = new BinaryReader(File.Open(FileName, FileMode.Open));
            try
            {
                string subject;
                bool profile;
                uint questionsCount;
                int difficulty;
                double passingScore;

                int count = reader.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    subject = reader.ReadString();
                    profile = reader.ReadBoolean();
                    questionsCount = reader.ReadUInt32();
                    difficulty = reader.ReadInt32();
                    passingScore = reader.ReadDouble();
                    tests.Add(new Tests(subject, profile, questionsCount, difficulty, passingScore));
                }
            }
            finally
            {
                reader.Close(); 
            }
            return tests;
        }

        public static void SaveDatabase(List<Tests> tests)
        {
            BinaryWriter writer = new BinaryWriter(File.Open(FileName, FileMode.Create));
            try
            {
                writer.Write(tests.Count);
                for (int i = 0; i < tests.Count; i++)
                {
                    Tests t = tests[i];
                    writer.Write(t.Subject);
                    writer.Write(t.Profile);
                    writer.Write(t.QuestionsCount);
                    writer.Write(t.Difficulty);
                    writer.Write(t.PassingScore);
                }
            }
            finally
            {
                writer.Close();
            }
        }


        public static void ViewDatabase(List<Tests> tests)
        {
            if (tests.Count == 0)
            {
                Console.WriteLine("База данных пуста.");
                return;
            }
            for (int i = 0; i < tests.Count; i++)
                Console.WriteLine(tests[i]);
        }

        public static void DeleteFromDatabase(List<Tests> tests, int deleteChoice, string value)
        {
            List<Tests> removeList = new List<Tests>();

            switch (deleteChoice)
            {
                case 1:
                    removeList = tests
                        .Where(t => t.Subject.Equals(value, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                    break;

                case 2:
                    if (!bool.TryParse(value, out bool boolVal))
                        throw new ArgumentException("Для поля «Профильный» введите true или false.");
                    removeList = tests
                        .Where(t => t.Profile == boolVal)
                        .ToList();
                    break;

                case 3:
                    if (!uint.TryParse(value, out uint uintVal))
                        throw new ArgumentException("Для поля «Кол-во заданий» введите неотрицательное целое.");
                    removeList = tests
                        .Where(t => t.QuestionsCount == uintVal)
                        .ToList();
                    break;

                case 4:
                    if (!int.TryParse(value, out int intVal))
                        throw new ArgumentException("Для поля «Сложность» введите целое число 0–10.");
                    removeList = tests
                        .Where(t => t.Difficulty == intVal)
                        .ToList();
                    break;

                case 5:
                    if (!double.TryParse(value, out double doubleVal))
                        throw new ArgumentException("Для поля «Проходной балл» введите неотрицательное число.");
                    removeList = tests
                        .Where(t => t.PassingScore == doubleVal)
                        .ToList();
                    break;

                default:
                    throw new ArgumentException("Неверный номер поля для удаления.");
            }

            
            foreach (Tests test in removeList)
            {
                tests.Remove(test);
            }
        }


        public static void AddTest(List<Tests> tests, Tests test)
            => tests.Add(test);

        public static List<Tests> TestsBySubject(List<Tests> tests, string subject)
            => tests.Where(t => t.Subject.Equals(subject, StringComparison.OrdinalIgnoreCase)).ToList();

        public static List<Tests> ProfileTests(List<Tests> tests)
            => tests.Where(t => t.Profile).ToList();

        public static double AveragePassingScore(List<Tests> tests)
        {
            if (!tests.Any())
                throw new InvalidOperationException("База данных пуста.");
            return tests.Average(t => t.PassingScore);
        }

        public static int CountByDifficulty(List<Tests> tests, int difficulty)
            => tests.Count(t => t.Difficulty == difficulty);
    }
}
