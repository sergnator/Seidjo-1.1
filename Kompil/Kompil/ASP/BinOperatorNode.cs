using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kompil.ASP
{
     class BinOperatorNode : ExpressionNode
    {
        

        public BinOperatorNode(Token Operator, ExpressionNode left, ExpressionNode right)
        {
            this.Operator = Operator;
            this.left = left;
            this.right = right;
        }
    }
}
