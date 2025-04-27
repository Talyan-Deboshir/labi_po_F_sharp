using System;

namespace Lab
{
    public static class InputValidator
    {
        public static byte InputByteWithValidation(string text, byte min, byte max)
        {
            bool ok;
            byte value;
            do
            {
                Console.WriteLine(text);
                ok = byte.TryParse(Console.ReadLine(), out value);
                if (ok && (value < min || value > max))
                    ok = false;
                if (!ok)
                {
                    var tmp = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nДолжно быть целое число в диапазоне [{min}–{max}]. Повторите.\n");
                    Console.ForegroundColor = tmp;
                }
            } while (!ok);
            return value;
        }

        public static uint InputUIntWithValidation(string text, int min, int max)
        {
            bool ok;
            uint value;
            do
            {
                Console.WriteLine(text);
                ok = uint.TryParse(Console.ReadLine(), out value);
                if (ok && (value < min || value > max))
                    ok = false;
                if (!ok)
                {
                    var tmp = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nДолжно быть неотрицательное целое. Повторите.\n");
                    Console.ForegroundColor = tmp;
                }
            } while (!ok);
            return value;
        }

        public static int InputIntWithValidation(string text, int min, int max)
        {
            bool ok;
            int value;
            do
            {
                Console.WriteLine(text);
                ok = int.TryParse(Console.ReadLine(), out value);
                if (ok && (value < min || value > max))
                    ok = false;
                if (!ok)
                {
                    var tmp = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nДолжно быть целое число в диапазоне [{min}–{max}]. Повторите.\n");
                    Console.ForegroundColor = tmp;
                }
            } while (!ok);
            return value;
        }

        public static double InputDoubleWithValidation(string text, double min = 0, double max = double.MaxValue)
        {
            bool ok;
            double value;
            do
            {
                Console.WriteLine(text);
                ok = double.TryParse(Console.ReadLine(), out value);
                if (ok && (value < min || value > max))
                    ok = false;
                if (!ok)
                {
                    var tmp = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nДолжно быть число в диапазоне [{min}–{max}]. Повторите.\n");
                    Console.ForegroundColor = tmp;
                }
            } while (!ok);
            return value;
        }

        public static bool InputBoolWithValidation(string text)
        {
            bool ok;
            bool value;
            do
            {
                Console.WriteLine(text + " (true/false)");
                ok = bool.TryParse(Console.ReadLine(), out value);
                if (!ok)
                {
                    var tmp = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nВведите true или false. Повторите.\n");
                    Console.ForegroundColor = tmp;
                }
            } while (!ok);
            return value;
        }

        public static string InputStringWithValidation(string text)
        {
            string value;
            do
            {
                Console.WriteLine(text);
                value = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(value))
                {
                    var tmp = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ввод не может быть пустым. Повторите.\n");
                    Console.ForegroundColor = tmp;
                }
            } while (string.IsNullOrWhiteSpace(value));
            return value;
        }
    }
}
