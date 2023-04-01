using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kompil
{
    class TokenType
    {
        public string name { get; set; }
        public string regex { get; set; }

        public TokenType() { }
        public TokenType(string name, string regex)
        {
            this.name = name;
            this.regex = regex;
        }
        public override string ToString()
        {
            return $"{name}";
        }

        public static Dictionary<string, TokenType>.ValueCollection tokenTypes => TokenTypes.Values;
        public static Dictionary<string, TokenType> TokenTypes = new Dictionary<string, TokenType>() {
            {"VAR", new TokenType("VAR", @"var") },
            {"SPACE", new TokenType("SPACE", @"[ /t/n/r]") },
            {"PLUS", new TokenType("PLUS", @"[+]") },
            {"END", new TokenType("END", @";") },
            {"PRINT", new TokenType("PRINT", @"ВЫВОД") }, 
            {"NUM", new TokenType("NUM", @"(-|)[1-9]+") },
            {"EQ", new TokenType("EQ", @"=") },
            {"MINUS", new TokenType("MINUS", @"[-]") },
            {"NAMEVAR", new TokenType("NAMEVAR", @"([a-z]|[A-Z])+") },
            {"LEFTSKOB", new TokenType("LEFTSKOB", @"[(]") },
            {"RIGHTSKOB", new TokenType("RIGHTSKOB", @"[)]") },
            {"INPUT", new TokenType("INPUT", @"ВВОД") },
            {"CONVERTTOINT", new TokenType("CONVERTTOINT", @"Конвертировать") }




        };
        
   }
    
}
