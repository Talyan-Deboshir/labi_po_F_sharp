using System;
using System.IO;

namespace Компилятор
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Работает Pascal-компилятор");

            string[] tests = { "test1.pas"};

            foreach (string test in tests)
            {
                if (!File.Exists(test))
                {
                    Console.WriteLine($"\nФайл {test} не найден.");
                    continue;
                }

                //Console.WriteLine($"Обработка {test}");
                InputOutput.file = new StreamReader(test);
                string baseName = Path.GetFileNameWithoutExtension(test);
                string outputFileName = baseName + "_out.txt";
                InputOutput.outputFile = new StreamWriter(outputFileName);
                //InputOutput.errList.Clear();
                InputOutput.positionNow = new TextPosition(1, 0);
                InputOutput.ReadNextLine();
                InputOutput.NextCh();

                LexicalAnalyzer analyzer = new LexicalAnalyzer();
                while (InputOutput.ch != '\0')
                {
                    byte token = analyzer.NextSym();
                    InputOutput.WriteToken(token);
                }

                InputOutput.End();

                InputOutput.file.Close();
                InputOutput.file = new StreamReader(test);
                InputOutput.positionNow = new TextPosition(1, 0);

                InputOutput.ReadNextLine();
                uint currentLine = 1;
                while (InputOutput.line != null)
                {
                    InputOutput.ListThisLine();   
                    //uint currentLine = InputOutput.positionNow.LineNumber;
                    InputOutput.ListErrorsForLine(currentLine);
                    currentLine++;
                    InputOutput.ReadNextLine();  
                }

                InputOutput.file.Close();
            }
        }
    }
}