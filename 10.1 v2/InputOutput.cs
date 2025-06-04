using System;
using System.IO;

namespace Компилятор
{
    class InputOutput
    {
        public static char ch 
        { 
            get; 
            set; 
        }
        public static TextPosition positionNow
        {
            get;
            set;
        }
        public static string line
        {
            get;
            set;
        }
        public static byte lastInLine
        {
            get;
            set;
        }
        public static StreamReader file
        {
            get;
            set;
        }
        public static StreamWriter outputFile
        {
            get;
            set;
        }
        public static List<ErrorInfo> errList
        {
            get;
            set;
        }
        static InputOutput()
        {
            positionNow = new TextPosition(1, 0);
            errList = new List<ErrorInfo>();
        }
        //public static List<ErrorInfo> errList = new List<ErrorInfo>();
        
        public static void NextCh()
        {
            if (line == null || positionNow.charNumber > line.Length)
            {
                ReadNextLine();

                if (line != null)
                {
                    positionNow.lineNumber++;
                    positionNow.charNumber = 0;
                }
                else
                {
                    ch = '\0';
                }
            }
            else if (positionNow.charNumber == line.Length)
            {
                ch = '\n';
                positionNow.charNumber++;
            }
            else
            {
                ch = line[positionNow.charNumber++];
            }
        }



        public static void ReadNextLine()
        {
            if (file != null && !file.EndOfStream)
            {
                line = file.ReadLine();
            }
            else
            {
                line = null;
            }
        }

        public static void WriteToken(byte tokenCode)
        {
            if (tokenCode != 0)
            {
                //Console.Write($"{tokenCode} ");
                if (outputFile != null)
                {
                    outputFile.Write($"{tokenCode} ");
                }
            }
        }
        public static void ListThisLine()
        {
            if (line != null)
                Console.WriteLine(line);
        }
        public static void End()
        {
            if (outputFile != null)
            {
                outputFile.Close();
            }
            Console.WriteLine("\n");
        }
        public static void AddError(int errorCode, string description)
        {
            var pos = new TextPosition(positionNow.LineNumber, positionNow.CharNumber);
            errList.Add(new ErrorInfo(pos.LineNumber, pos.CharNumber, errorCode, description));
        }
        public static void ListErrorsForLine(uint currentLineNumber)
        {
            var errorsOnLine = errList.Where(e => e.Line == currentLineNumber).ToList();
            int errCount = 0;

            foreach (var item in errorsOnLine)
            {
                string errorArrow = $"^";

                string description = item.Description;
                if (string.IsNullOrEmpty(description))
                {
                    description = "неизвестная ошибка";
                }
                int spaces = 0;
                string leadingSpaces = "";
                for (int i = 0; i < item.Column && i < line.Length; i++)
                {
                    if (line[i] == '\t')
                        spaces += 4;
                }

                if (spaces != 0)
                {
                    leadingSpaces = new string(' ', item.Column+spaces);
                }
                else
                {
                    leadingSpaces = new string(' ', item.Column);
                }
                string s = $"{leadingSpaces}{errorArrow} ошибка код {item.Code} ({description})";
                Console.WriteLine(s);
            }
        }
    }
}