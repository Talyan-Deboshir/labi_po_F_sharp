using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Компилятор
{
    struct TextPosition
    {
        public uint lineNumber;
        public byte charNumber;

        public TextPosition(uint ln = 0, byte c = 0)
        {
            lineNumber = ln;
            charNumber = c;
        }
    }

    struct Err
    {
        public TextPosition errorPosition;
        public byte errorCode;

        public Err(TextPosition errorPosition, byte errorCode)
        {
            this.errorPosition = errorPosition;
            this.errorCode = errorCode;
        }
    }

    class InputOutput
    {
        const byte ERRMAX = 9;
        public static char Ch 
        { 
            get;
            set; 
        }
        public static TextPosition positionNow = new TextPosition(1, 0);
        public static string line;
        public static byte lastInLine = 0;
        public static List<Err> err;
        public static StreamReader File 
        { 
            get; 
            set; 
        }
        public static uint errCount = 0;
        public static uint totalErrors = 0;

        public static List<(uint lineNumber, Err error)> expectedErrors = new List<(uint, Err)>();

        
        public static List<(byte code, string description)> ErrorDescriptions = new List<(byte, string)>
        {
            (100, "использование имени не соответствует описанию"),
            (147, "тип метки не совпадает с типом выбирающего выражения"),
            (201, "отсутствует точка с запятой")
        };

        public static void NextCh()
        {
            if (line == null || positionNow.charNumber >= line.Length)
            {
                if (line != null)
                {
                    ListThisLine();
                    if (err != null && err.Count > 0)
                        ListErrors();
                }
                ReadNextLine();
                if (line == null)
                {
                    Ch = '\0';
                    return;
                }
                positionNow.lineNumber++;
                positionNow.charNumber = 0;
                lastInLine = (byte)line.Length;

                
                err = new List<Err>();
                var errorsForLine = expectedErrors.Where(e => e.lineNumber == positionNow.lineNumber);
                foreach (var error in errorsForLine)
                {
                    if (totalErrors < ERRMAX)
                    {
                        err.Add(error.error);
                        totalErrors++;
                    }
                }
            }
            else
            {
                positionNow.charNumber++;
            }

            if (positionNow.charNumber < line.Length)
            {
                Ch = line[positionNow.charNumber];
            }
            else
            {
                Ch = '\n';
            }
        }

        public static void ListThisLine()
        {
            if (line != null)
                Console.WriteLine(line);
        }

        public static void ReadNextLine()
        {
            if (File != null && !File.EndOfStream)
            {
                line = File.ReadLine();
            }
            else
            {
                line = null;
            }
        }

        public static void End()
        {
            Console.WriteLine($"Компиляция завершена: ошибок — {totalErrors}!");
        }

        public static void ListErrors()
        {
            foreach (Err item in err)
            {
                errCount++;
                string errorNumber = $"**{errCount}**";

                string description = ErrorDescriptions.FirstOrDefault(d => d.code == item.errorCode).description;
                if (string.IsNullOrEmpty(description))
                    description = "неизвестная ошибка";

                string s = $"{errorNumber} ошибка код {item.errorCode} ({description})";
                Console.WriteLine(s);
            }
        }
    }
}