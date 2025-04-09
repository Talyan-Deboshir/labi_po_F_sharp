using System;

namespace Lab
{
    public class InputDataWithCheck
    {
        public static bool InputBoolWithValidation(string text)
        {
            bool ok;
            bool result;
            do
            {
                Console.WriteLine(text);
                string input = Console.ReadLine();
                ok = bool.TryParse(input, out result);
                if (!ok)
                {
                    ConsoleColor tmp = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nОшибка: введите 'true' или 'false'.");
                    Console.WriteLine("Повторите ввод\n");
                    Console.ForegroundColor = tmp;
                }
            } while (!ok);
            return result;
        }

        public static int InputIntegerWithValidation(string text, int min, int max)
        {
            bool ok;
            int result;
            do
            {
                Console.WriteLine(text);
                string input = Console.ReadLine();
                ok = int.TryParse(input, out result) && result >= min && result <= max;
                if (!ok)
                {
                    ConsoleColor tmp = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nОшибка: введите число от {min} до {max}.");
                    Console.WriteLine("Повторите ввод\n");
                    Console.ForegroundColor = tmp;
                }
            } while (!ok);
            return result;
        }

        public static char InputGenderWithValidation(string text)
        {
            char gender;
            bool ok;
            do
            {
                Console.WriteLine(text);
                string input = Console.ReadLine();
                ok = input.Length == 1 && (input == "M" || input == "F");
                if (ok)
                {
                    gender = input[0];
                }
                else
                {
                    ConsoleColor tmp = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nОшибка: введите 'M' или 'F'.");
                    Console.WriteLine("Повторите ввод\n");
                    Console.ForegroundColor = tmp;
                    gender = ' ';
                }
            } while (!ok);
            return gender;
        }
    }
}