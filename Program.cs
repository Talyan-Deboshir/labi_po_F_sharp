namespace Lab
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Создание объекта базового класса:");
            bool haveJob = InputDataWithCheck.InputBoolWithValidation("Есть ли у вас работа? (true/false):");
            bool haveRussianCitizenship = InputDataWithCheck.InputBoolWithValidation("Есть ли у вас гражданство РФ? (true/false):");
            Logic jobAndCit = new Logic(haveJob, haveRussianCitizenship);
            Console.WriteLine($"Базовый объект: {jobAndCit}");
            Console.WriteLine($"Эквивалентность полей: {jobAndCit.Equivalent()}");

            Logic jobAndCitCopy = new Logic(jobAndCit);
            Console.WriteLine($"Копия базового объекта: {jobAndCitCopy}");

            Console.WriteLine("\nСоздание объекта дочернего класса:");
            int age = InputDataWithCheck.InputIntegerWithValidation("Введите возраст (от 14 до 100):", 14, 100);
            char gender = InputDataWithCheck.InputGenderWithValidation("Введите пол (M или F):");
            User user = new User(haveJob, haveRussianCitizenship, age, gender);
            Console.WriteLine("Информация о пользователе: ");
            user.PrintUserInfo();

            User userCopy = new User(user);
            Console.WriteLine("Информация о копии пользователя: ");
            userCopy.PrintUserInfo();
        }
    }
}