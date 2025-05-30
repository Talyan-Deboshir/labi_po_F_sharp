using System;

namespace Lab
{
    public class Time
    {
        private byte hours;
        private byte minutes;


        public byte Hours
        {
            get 
            { 
                return hours; 
            }
            set
            {
                if (value > 23)
                {
                    throw new ArgumentException($"Часы должны быть между 0 и 23.");
                }
                else
                { 
                    hours = value;
                }
            }
        }

        public byte Minutes
        {
            get 
            { 
                return minutes; 
            }
            set
            {
                if (value > 59)
                {
                    throw new ArgumentException($"Минуты должны быть между 0 и 59.");
                }
                else
                {
                    minutes = value;
                }
            }
        }


        public Time() 
        {
            Hours = 0;
            Minutes = 0;
        }
        public Time(byte hours, byte minutes)
        {
            Hours = hours;
            Minutes = minutes;
        }


        public int TotalMinutes()
        {
            return hours * 60 + minutes;
        }

        public static Time operator -(Time time1, Time time2)
        {
            int totalMinutes1 = time1.TotalMinutes();
            int totalMinutes2 = time2.TotalMinutes();
            int diff = (totalMinutes1 - totalMinutes2 + 1440) % 1440;
            byte newHours = (byte)(diff / 60);
            byte newMinutes = (byte)(diff % 60);
            return new Time(newHours, newMinutes);
        }

        public static Time operator +(Time time1, Time time2)
        {
            int totalMinutes1 = time1.TotalMinutes();
            int totalMinutes2 = time2.TotalMinutes();
            int sum = (totalMinutes1 + totalMinutes2) % 1440;
            byte newHours = (byte)(sum / 60);
            byte newMinutes = (byte)(sum % 60);
            return new Time(newHours, newMinutes);
        }

        public static Time operator +(Time t, uint minutes)
        {
            int totalMin = t.TotalMinutes() + (int)minutes;
            totalMin %= 1440;
            byte newHours = (byte)(totalMin / 60);
            byte newMinutes = (byte)(totalMin % 60);
            return new Time(newHours, newMinutes);
        }

        public static Time operator +(uint minutes, Time t)
        {
            return t + minutes;
        }

        public static Time operator --(Time t)
        {
            if (t.minutes > 0)
            {
                return new Time(t.hours, (byte)(t.minutes - 1));
            }
            else if (t.hours > 0)
            {
                return new Time((byte)(t.hours - 1), 59);
            }
            else
            {
                return new Time(23, 59);
            }
        }

        
        public static explicit operator byte(Time t)
        {
            return t.hours;
        }

        
        public static implicit operator bool(Time t)
        {
            return t.hours != 0 || t.minutes != 0;
        }

        public override string ToString()
        {
            return $"{hours:D2}:{minutes:D2}";
        }
    }
}
