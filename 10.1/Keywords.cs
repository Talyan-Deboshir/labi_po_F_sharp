using System.Collections.Generic;

namespace Компилятор
{
    class Keywords
    {
        Dictionary<byte, Dictionary<string, byte>> kw 
        { 
            get; 
            set; 
        }

        public Dictionary<byte, Dictionary<string, byte>> Kw 
        { 
            get 
            { 
                return kw; 
            } 
        }

        public Keywords()
        {
            kw = new Dictionary<byte, Dictionary<string, byte>>();
            Dictionary<string, byte> tmp;

            tmp = new Dictionary<string, byte>
            {
                ["do"] = LexicalAnalyzer.dosy,
                ["if"] = LexicalAnalyzer.ifsy,
                ["in"] = LexicalAnalyzer.insy,
                ["of"] = LexicalAnalyzer.ofsy,
                ["or"] = LexicalAnalyzer.orsy,
                ["to"] = LexicalAnalyzer.tosy
            };
            kw[2] = tmp;

            tmp = new Dictionary<string, byte>
            {
                ["end"] = LexicalAnalyzer.endsy,
                ["var"] = LexicalAnalyzer.varsy,
                ["div"] = LexicalAnalyzer.divsy,
                ["and"] = LexicalAnalyzer.andsy,
                ["not"] = LexicalAnalyzer.notsy,
                ["for"] = LexicalAnalyzer.forsy,
                ["mod"] = LexicalAnalyzer.modsy,
                ["nil"] = LexicalAnalyzer.nilsy,
                ["set"] = LexicalAnalyzer.setsy
            };
            kw[3] = tmp;

            tmp = new Dictionary<string, byte>
            {
                ["then"] = LexicalAnalyzer.thensy,
                ["else"] = LexicalAnalyzer.elsesy,
                ["case"] = LexicalAnalyzer.casesy,
                ["file"] = LexicalAnalyzer.filesy,
                ["goto"] = LexicalAnalyzer.gotosy,
                ["type"] = LexicalAnalyzer.typesy,
                ["with"] = LexicalAnalyzer.withsy,
                ["real"] = LexicalAnalyzer.realsy,
                ["sqrt"] = LexicalAnalyzer.sqrtsy
            };
            kw[4] = tmp;

            tmp = new Dictionary<string, byte>
            {
                ["begin"] = LexicalAnalyzer.beginsy,
                ["while"] = LexicalAnalyzer.whilesy,
                ["array"] = LexicalAnalyzer.arraysy,
                ["const"] = LexicalAnalyzer.constsy,
                ["label"] = LexicalAnalyzer.labelsy,
                ["until"] = LexicalAnalyzer.untilsy,
                ["write"] = LexicalAnalyzer.writesy
            };
            kw[5] = tmp;

            tmp = new Dictionary<string, byte>
            {
                ["downto"] = LexicalAnalyzer.downtosy,
                ["packed"] = LexicalAnalyzer.packedsy,
                ["record"] = LexicalAnalyzer.recordsy,
                ["repeat"] = LexicalAnalyzer.repeatsy,
                ["random"] = LexicalAnalyzer.randomsy,
                ["readln"] = LexicalAnalyzer.readlnsy
            };
            kw[6] = tmp;

            tmp = new Dictionary<string, byte>
            {
                ["program"] = LexicalAnalyzer.programsy,
                ["integer"] = LexicalAnalyzer.integersy,
                ["writeln"] = LexicalAnalyzer.writelnsy
            };
            kw[7] = tmp;

            tmp = new Dictionary<string, byte>
            {
                ["function"] = LexicalAnalyzer.functionsy
            };
            kw[8] = tmp;

            tmp = new Dictionary<string, byte>
            {
                ["procedure"] = LexicalAnalyzer.procedurensy,
                ["randomize"] = LexicalAnalyzer.randomizesy
            };
            kw[9] = tmp;
        }
    }
}