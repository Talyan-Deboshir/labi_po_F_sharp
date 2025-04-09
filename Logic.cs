namespace Lab
{
    public class Logic
    {
        private bool haveJob;
        private bool haveRussianCitizenship;

        public bool HaveJob
        {
            get { return haveJob; }
            set { haveJob = value; }
        }

        public bool HaveRussianCitizenship
        {
            get { return haveRussianCitizenship; }
            set { haveRussianCitizenship = value; }
        }

        public Logic()
        {
            HaveJob = false;
            HaveRussianCitizenship = false;
        }

        public Logic(bool haveJob, bool haveRussianCitizenship)
        {
            HaveJob = haveJob;
            HaveRussianCitizenship = haveRussianCitizenship;
        }

        public Logic(Logic other)
        {
            HaveJob = other.HaveJob;
            HaveRussianCitizenship = other.HaveRussianCitizenship;
        }

        public bool Equivalent()
        {
            return HaveJob == HaveRussianCitizenship;
        }

        public override string ToString()
        {
            return $"HaveJob: {HaveJob}, HaveRussianCitizenship: {HaveRussianCitizenship}";
        }
    }
}