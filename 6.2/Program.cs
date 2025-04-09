using System;

namespace Lab
{
    class Program
    {
        static void Main()
        {
           
            Console.WriteLine("Введите первое время:");
            byte hours1 = InputDataWithCheck.InputByteWithValidation("Часы (от 0 до 23): ", 0, 23);
            byte minutes1 = InputDataWithCheck.InputByteWithValidation("Минуты (от 0 до 59): ", 0, 59);
            Time time1 = new Time(hours1, minutes1);

            Console.WriteLine("Введите второе время:");
            byte hours2 = InputDataWithCheck.InputByteWithValidation("Часы (от 0 до 23): ", 0, 23);
            byte minutes2 = InputDataWithCheck.InputByteWithValidation("Минуты (от 0 до 59): ", 0, 59);
            Time time2 = new Time(hours2, minutes2);

            Console.WriteLine($"Время 1: {time1}");
            Console.WriteLine($"Время 2: {time2}");

            Time diff = time1 - time2;
            Console.WriteLine($"Разница времён: {diff}");

            Time sum = time1 + time2;
            Console.WriteLine($"Сумма времён (time1 + time2): {sum}");

            uint plusMinutes = InputDataWithCheck.InputUIntWithValidation("Введите количество минут для добавления: ");
            Time time1PlusMinutes = time1 + plusMinutes;
            Console.WriteLine($"Время 1 + {plusMinutes} минут: {time1PlusMinutes}");

            Time time1Copy = new Time(time1.Hours, time1.Minutes);
            --time1Copy;
            Console.WriteLine($"Время 1 после вычитания минуты: {time1Copy}");

            byte hoursFromTime1 = (byte)time1;
            Console.WriteLine($"Часы из времени 1: {hoursFromTime1}");

            // Приведение к bool (проверка на 00:00)
            bool ZeroTimeChecker = time1;
            Console.WriteLine($"Время 1 не равно 00:00: {ZeroTimeChecker}");
        }
    }
}