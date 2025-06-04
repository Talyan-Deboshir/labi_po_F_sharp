namespace Компилятор
{
    class ErrorInfo
    {
        public uint Line 
        { 
            get; 
        }
        public byte Column 
        { 
            get; 
        }
        public int Code 
        { 
            get; 
        }
        public string Description 
        { 
            get; 
        }

        public ErrorInfo(uint line, byte column, int code, string descr)
        {
            Line = line;
            Column = column;
            Code = code;
            Description = descr;
        }
    }
}
