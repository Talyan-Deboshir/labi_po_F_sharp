using System;

namespace Компилятор
{
    class LexicalAnalyzer
    {
        public const byte
            star = 21, // *
            slash = 60, // /
            equal = 16, // =
            comma = 20, // ,
            semicolon = 14, // ;
            colon = 5, // :
            point = 61, // .
            arrow = 62, // ^
            leftpar = 9, // (
            rightpar = 4, // )
            lbracket = 11, // [
            rbracket = 12, // ]
            flpar = 63, // {
            frpar = 64, // }
            later = 65, // <
            greater = 66, // >
            laterequal = 67, // <=
            greaterequal = 68, // >=
            latergreater = 69, // <>
            plus = 70, // +
            minus = 71, // –
            lcomment = 72, // (*
            rcomment = 73, // *)
            assign = 51, // :=
            twopoints = 74, // ..
            ident = 2, // идентификатор
            floatc = 82, // вещественная константа
            intc = 15, // целая константа
            casesy = 31,
            elsesy = 32,
            filesy = 57,
            gotosy = 33,
            thensy = 52,
            typesy = 34,
            untilsy = 53,
            dosy = 54,
            withsy = 37,
            ifsy = 56,
            insy = 100,
            ofsy = 101,
            orsy = 102,
            tosy = 103,
            endsy = 104,
            varsy = 105,
            divsy = 106,
            andsy = 107,
            notsy = 108,
            forsy = 109,
            modsy = 110,
            nilsy = 111,
            setsy = 112,
            beginsy = 113,
            whilesy = 114,
            arraysy = 115,
            constsy = 116,
            labelsy = 117,
            downtosy = 118,
            packedsy = 119,
            recordsy = 120,
            repeatsy = 121,
            programsy = 122,
            functionsy = 123,
            procedurensy = 124,
            integersy = 125,
            writesy = 126,
            randomizesy = 127,
            randomsy = 128,
            realsy = 129,
            readlnsy = 130,
            sqrtsy = 131,
            writelnsy = 132,
            charc = 133;

        private byte Symbol 
        { 
            get; 
            set; 
        }
        private TextPosition Token 
        { 
            get; 
            set; 
        }
        private string AddrName 
        { 
            get;
            set; 
        }
        private int Nmb_int
        {
            get;
            set;
        }
        private float Nmb_float
        {
            get;
            set;
        }
        private char One_symbol
        {
            get;
            set;
        }
        private Keywords Keyword
        {
            get;
            set;
        }

        public LexicalAnalyzer()
        {
            Keyword = new Keywords();
        }

        public byte NextSym()
        {
            while (InputOutput.ch == ' ' || InputOutput.ch == '\t' || InputOutput.ch == '\n')
            {
                InputOutput.NextCh();
            }

            Token = new TextPosition
            {
                LineNumber = InputOutput.positionNow.LineNumber,
                CharNumber = InputOutput.positionNow.CharNumber
            };

          

           

            switch (InputOutput.ch)
            {
                case '%':
                    //Console.WriteLine($"Запрещенный символ '{InputOutput.ch}' в позиции {Token.LineNumber}:{Token.CharNumber}");
                    InputOutput.AddError(1, "Запрещенный символ");
                    InputOutput.NextCh();
                    Symbol = 0;
                    break;
                case '&':
                    //Console.WriteLine($"Запрещенный символ '{InputOutput.ch}' в позиции {Token.LineNumber}:{Token.CharNumber}");
                    InputOutput.AddError(1, "Запрещенный символ");
                    InputOutput.NextCh();
                    Symbol = 0;
                    break;
                case '$':
                    //Console.WriteLine($"Запрещенный символ '{InputOutput.ch}' в позиции {Token.LineNumber}:{Token.CharNumber}");
                    InputOutput.AddError(1, "Запрещенный символ");
                    InputOutput.NextCh();
                    Symbol = 0;
                    break;
                case '?':
                    //Console.WriteLine($"Запрещенный символ '{InputOutput.ch}' в позиции {Token.LineNumber}:{Token.CharNumber}");
                    InputOutput.AddError(1, "Запрещенный символ");
                    InputOutput.NextCh();
                    Symbol = 0;
                    break;
                case '\'':
                    InputOutput.NextCh();
                    while (InputOutput.ch != '\'' && InputOutput.ch != '\0' && InputOutput.ch != '\n')
                    {
                        InputOutput.NextCh();
                    }
                    if (InputOutput.ch == '\'')
                    {
                        InputOutput.NextCh();
                    }
                    else
                    {
                        //Console.WriteLine($"Отсутствует закрывающая ковычка '\'' в позиции {Token.LineNumber}:{Token.CharNumber}");
                        InputOutput.AddError(2, "Отсутствует закрывающая ковычка");
                    }
                    Symbol = NextSym();
                    break;


                case '{':
                    InputOutput.NextCh();
                    while (InputOutput.ch != '}' && InputOutput.ch != '\0' && InputOutput.ch != '\n')
                    {
                        InputOutput.NextCh();
                    }
                    if (InputOutput.ch == '}')
                    {
                        InputOutput.NextCh();
                    }
                    else
                    {
                        //Console.WriteLine($"Отсутствует закрывающая фигурная скобка '}}' в позиции {Token.LineNumber}:{Token.CharNumber}");
                        InputOutput.AddError(3, "Отсутствует закрывающая фигурная скобка");
                    }
                    Symbol = NextSym();
                    break;

                case '/':
                    InputOutput.NextCh();
                    if (InputOutput.ch == '/')
                    {
                        while (InputOutput.ch != '\n' && InputOutput.ch != '\0')
                        {
                            InputOutput.NextCh();
                        }
                        Symbol = NextSym();
                    }
                    else
                    {
                        Symbol = slash;
                    }
                    break;

                case char c when char.IsDigit(c):
                    Int16 maxint = Int16.MaxValue;
                    Nmb_int = 0;
                    Nmb_float = 0;
                    bool overflow = false;
                    
                    while (char.IsDigit(InputOutput.ch))
                    {
                        byte digit = (byte)(InputOutput.ch - '0');
                        if (!overflow && Nmb_int <= (maxint - digit) / 10)
                        {
                            Nmb_int = 10 * Nmb_int + digit;
                        }
                        else
                        {
                            overflow = true;
                        }
                        InputOutput.NextCh();
                    }
                    
                    if (InputOutput.ch == '.')
                    {
                        InputOutput.NextCh();
                        Nmb_float = Nmb_int;
                        float fraction = 0.1f;
                        
                        while (char.IsDigit(InputOutput.ch))
                        {
                            Nmb_float += (InputOutput.ch - '0') * fraction;
                            fraction /= 10;
                            InputOutput.NextCh();
                        }
                        Symbol = floatc;
                    }
                    else
                    {
                        if (overflow)
                        {
                            //Console.WriteLine($"Целая константа превышает предел в позиции {Token.LineNumber}:{Token.CharNumber}");
                            InputOutput.AddError(4, "Целая константа превышает предел");
                        }
                        Symbol = intc;
                    }
                    break;

                case char c when char.IsLetter(c):
                    string name = "";
                    while (char.IsLetterOrDigit(InputOutput.ch))
                    {
                        name += InputOutput.ch;
                        InputOutput.NextCh();
                    }
                    
                    name = name.ToLower();
                    Symbol = ident;
                    if (Keyword.Kw.ContainsKey((byte)name.Length) && Keyword.Kw[(byte)name.Length].ContainsKey(name))
                    {
                        Symbol = Keyword.Kw[(byte)name.Length][name];
                    }
                    /*
                    name = name.ToLower();
                    Console.WriteLine($"Processing: '{name}'");
                    if (keywords.Kw.ContainsKey((byte)name.Length) && keywords.Kw[(byte)name.Length].ContainsKey(name))
                    {
                        symbol = keywords.Kw[(byte)name.Length][name];
                        Console.WriteLine($"Keyword: {symbol}");
                    }
                    else
                    {
                        symbol = ident;
                        Console.WriteLine("Identifier");
                    }
                    */

                    AddrName = name;
                    break;

                case '*':
                    Symbol = star;
                    InputOutput.NextCh();
                    break;
/*
                case '/':
                    Symbol = slash;
                    InputOutput.NextCh();
                    break;
*/
                case '=':
                    Symbol = equal;
                    InputOutput.NextCh();
                    break;

                case ',':
                    Symbol = comma;
                    InputOutput.NextCh();
                    break;

                case ';':
                    Symbol = semicolon;
                    InputOutput.NextCh();
                    break;

                case ':':
                    InputOutput.NextCh();
                    if (InputOutput.ch == '=')
                    {
                        Symbol = assign;
                        InputOutput.NextCh();
                    }
                    else
                    {
                        Symbol = colon;
                    }
                    break;

                case '.':
                    InputOutput.NextCh();
                    if (InputOutput.ch == '.')
                    {
                        Symbol = twopoints;
                        InputOutput.NextCh();
                    }
                    else
                    {
                        Symbol = point;
                    }
                    break;

                case '^':
                    Symbol = arrow;
                    InputOutput.NextCh();
                    break;

                case '(':
                    InputOutput.NextCh();
                    if (InputOutput.ch == '*')
                    {
                        Symbol = lcomment;
                        InputOutput.NextCh();
                    }
                    else
                    {
                        Symbol = leftpar;
                    }
                    break;

                case ')':
                    InputOutput.NextCh();
                    if (InputOutput.ch == '*')
                    {
                        Symbol = rcomment;
                        InputOutput.NextCh();
                    }
                    else
                    {
                        Symbol = rightpar;
                    }
                    break;

                case '[':
                    Symbol = lbracket;
                    InputOutput.NextCh();
                    break;

                case ']':
                    Symbol = rbracket;
                    InputOutput.NextCh();
                    break;

                

                case '}':
                    Symbol = frpar;
                    InputOutput.NextCh();
                    break;

                case '<':
                    InputOutput.NextCh();
                    if (InputOutput.ch == '=')
                    {
                        Symbol = laterequal;
                        InputOutput.NextCh();
                    }
                    else if (InputOutput.ch == '>')
                    {
                        Symbol = latergreater;
                        InputOutput.NextCh();
                    }
                    else
                    {
                        Symbol = later;
                    }
                    break;

                case '>':
                    InputOutput.NextCh();
                    if (InputOutput.ch == '=')
                    {
                        Symbol = greaterequal;
                        InputOutput.NextCh();
                    }
                    else
                    {
                        Symbol = greater;
                    }
                    break;

                case '+':
                    Symbol = plus;
                    InputOutput.NextCh();
                    break;

                case '-':
                    Symbol = minus;
                    InputOutput.NextCh();
                    break;

                default:
                    InputOutput.NextCh();
                    return 0;
            }

            return Symbol;
        }
    }
}