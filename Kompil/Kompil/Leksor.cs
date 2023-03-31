using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Kompil
{
    class Leksor
    {
        public string code { get; set; }
        public List<Token> tokens = new List<Token>();
        public int pos = 0;
        

        public Leksor(string code) {
            
            this.code = code;
        }
        
        public List<Token> lexAnalytic() 
        {
            while (NextToken())
            {
                
            }
            return tokens;
        }
        public bool NextToken()
        {
            if(pos >= code.Length) return false;

            foreach (var tokenType in TokenType.tokenTypes)
            {
                if (pos >= code.Length) return false;
                
                Regex regex = new Regex(@"^" + tokenType.regex);
                MatchCollection result = regex.Matches(code.Substring(pos));
                
                if(result.Count > 0) 
                {
                    Token token = new Token(tokenType, result[0].Value);
                    pos += result[0].Value.Length;
                    if (result[0].Length == 0) 
                    {
                        pos += 1;
                    }
                    tokens.Add(token);
                    return true;
                }
                
           }
           throw new Exception($"На позиции {pos} неизвестный синтаксис");

        }

    }
}
