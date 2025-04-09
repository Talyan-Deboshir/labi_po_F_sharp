namespace Lab
{
    public class User : Logic
    {
        private int age;
        private char gender;

        public int Age
        {
            get { return age; }
            set { age = value; }
        }

        public char Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        public User() : base()
        {
            Age = 0;
            Gender = ' ';
        }

        public User(bool haveJob, bool haveRussianCitizenship, int age, char gender)
            : base(haveJob, haveRussianCitizenship)
        {
            Age = age;
            Gender = gender;
        }

        public User(User other) : base(other)
        {
            Age = other.Age;
            Gender = other.Gender;
        }

        public bool IsAdult()
        {
            return Age >= 18;
        }

        public void PrintUserInfo()
        {
            Console.WriteLine($"Возраст: {Age}, Пол: {Gender}, Совершеннолетний: {IsAdult()}, Есть работа: {HaveJob}, Есть гражданство РФ: {HaveRussianCitizenship}");
        }
    }
}