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

        static InputOutput()
        {
            positionNow = new TextPosition(1, 0);
        }

        public static void NextCh()
        {
            if (line == null || positionNow.charNumber >= line.Length)
            {
                if (line != null)
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
                        return;
                    }
                }
                else
                {
                    ch = '\0';
                    return;
                }
            }
            ch = line[positionNow.charNumber];
            positionNow.charNumber++;
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
                Console.Write($"{tokenCode} ");
                if (outputFile != null)
                {
                    outputFile.Write($"{tokenCode} ");
                }
            }
        }

        public static void End()
        {
            if (outputFile != null)
            {
                outputFile.Close();
            }
            Console.WriteLine("\n");
        }
    }
}