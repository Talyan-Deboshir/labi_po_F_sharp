using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Компилятор
{
    class TextPosition
    {
        public uint lineNumber
        {
            get;
            set;
        }
        public byte charNumber
        {
            get;
            set;
        }

        public uint LineNumber
        {
            get
            {
                return lineNumber;
            }
            set
            {
                lineNumber = value;
            }
        }
        public byte CharNumber
        {
            get
            {
                return charNumber;
            }
            set
            {
                charNumber = value;
            }
        }

        public TextPosition(uint ln = 0, byte c = 0)
        {
            lineNumber = ln;
            charNumber = c;
        }
    }
}
