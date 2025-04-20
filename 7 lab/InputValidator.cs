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
                if (ok)
                {
                    if (value < min || value > max)
                    {
                        ok = false;
                    }
                }
                if (!ok)
                {
                    ConsoleColor tmp = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nВведенные данные имеют неверный формат или не принадлежат диапазону [{min}; {max}]");
                    Console.WriteLine("Повторите ввод\n");
                    Console.ForegroundColor = tmp;
                }
            } while (!ok);
            return value;
        }

        
        public static uint InputUIntWithValidation(string text)
        {
            bool ok;
            uint value;
            do
            {
                Console.WriteLine(text);
                ok = uint.TryParse(Console.ReadLine(), out value);
                if (!ok)
                {
                    ConsoleColor tmp = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nВведенные данные имеют неверный формат. Введите неотрицательное целое число.");
                    Console.WriteLine("Повторите ввод\n");
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
                if (ok)
                {
                    if (value < min || value > max)
                    {
                        ok = false;
                    }
                }
                if (!ok)
                {
                    ConsoleColor tmp = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\nВведенные данные имеют неверный формат или не принадлежат диапазону [{min}; {max}]");
                    Console.WriteLine("Повторите ввод\n");
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
                    ConsoleColor tmp = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Повторите ввод\n");
                    Console.ForegroundColor = tmp;
                }
                else
                {
                    break;
                }
            } while (true);
            return value;
        }
    }
}