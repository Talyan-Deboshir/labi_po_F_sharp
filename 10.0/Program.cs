using System;
using System.Collections.Generic;
using System.IO;

namespace Компилятор
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Работает Pascal-компилятор");

            string[] tests = { "test1.pas", "test2.pas", "test3.pas", "test4.pas", "test5.pas" };

            foreach (string test in tests)
            {
                if (!File.Exists(test))
                {
                    Console.WriteLine($"\nFile {test} not found. Skipping...");
                    continue;
                }

                Console.WriteLine($"\nProcessing {test}");
                InputOutput.expectedErrors = new List<(uint, Err)>();
                InputOutput.totalErrors = 0;

                switch (test)
                {
                    case "test2.pas":
                        InputOutput.expectedErrors.Add((3, new Err(new TextPosition(3, 6), 201)));
                        break;
                    case "test3.pas":
                        InputOutput.expectedErrors.Add((10, new Err(new TextPosition(10, 5), 100)));
                        InputOutput.expectedErrors.Add((12, new Err(new TextPosition(12, 5), 100)));
                        InputOutput.expectedErrors.Add((13, new Err(new TextPosition(13, 5), 147)));
                        InputOutput.expectedErrors.Add((14, new Err(new TextPosition(14, 5), 147)));

                        InputOutput.expectedErrors.Add((14, new Err(new TextPosition(14, 6), 100)));
                        break;
                    case "test5.pas":
                        for (uint i = 3; i <= 11; i++)
                        {
                            InputOutput.expectedErrors.Add((i, new Err(new TextPosition(i, 6), 201)));
                        }
                        break;
                    default:
                        break;
                }

                InputOutput.File = new System.IO.StreamReader(test);
                InputOutput.positionNow = new TextPosition(1, 0);
                InputOutput.errCount = 0;
                InputOutput.ReadNextLine();
                InputOutput.NextCh();

                while (InputOutput.Ch != '\0')
                {
                    InputOutput.NextCh();
                }

                if (InputOutput.line != null)
                {
                    InputOutput.ListThisLine();
                    if (InputOutput.err != null && InputOutput.err.Count > 0)
                        InputOutput.ListErrors();
                }

                InputOutput.End();
            }
        }
    }
}