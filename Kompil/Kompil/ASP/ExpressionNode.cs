using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kompil.ASP
{
     class ExpressionNode
    {
        public string text { get; set; }
        public Token Operator { get; set; }
        public ExpressionNode Operand { get; set; }
        public ExpressionNode left { get; set; }
        public ExpressionNode right { get; set; }
        public Token var;
        public List<ExpressionNode> codeStrings = new List<ExpressionNode>();
        public Token number { get; set; }
    }
}
