using System;

namespace Lab
{
    [Serializable]
    public class Tests
    {
        private string subject;
        private bool profile;
        private uint questionsCount;
        private int difficulty;
        private double passingScore;

        public string Subject
        {
            get
            {
                return subject;
            }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Название предмета не может быть пустым.");
                subject = value;
            }
        }

        public bool Profile
        {
            get
            {
                return profile;
            }
            set
            {
                profile = value;
            }
        }

        public uint QuestionsCount
        {
            get
            {
                return questionsCount;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Количество вопросов не может быть отрицательным.");
                questionsCount = value;
            }
        }

        public int Difficulty
        {
            get
            {
                return difficulty;
            }
            set
            {
                if (value < 0 || value > 10)
                    throw new ArgumentException("Сложность должна быть в диапазоне 0–10.");
                difficulty = value;
            }
        }

        public double PassingScore
        {
            get
            {
                return passingScore;
            }
            set
            {
                if (value < 0)
                    throw new ArgumentException("Проходной балл не может быть отрицательным.");
                passingScore = value;
            }
        }

        public Tests() { }

        public Tests(string subject, bool profile, uint questionsCount, int difficulty, double passingScore)
        {
            Subject = subject;
            Profile = profile;
            QuestionsCount = questionsCount;
            Difficulty = difficulty;
            PassingScore = passingScore;
        }

        public override string ToString()
        {
            return $"Предмет: {Subject}, Профильный: {Profile}, " +
                   $"Количество заданий: {QuestionsCount}, Сложность: {Difficulty}, " +
                   $"Проходной балл: {PassingScore}";
        }
    }
}
