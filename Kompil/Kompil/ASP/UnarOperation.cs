using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kompil.ASP
{
    class UnarOperationNode : ExpressionNode
    {

        public UnarOperationNode(Token @operator, ExpressionNode operand)
        {
            Operator = @operator;
            Operand = operand;
        }
    }
}
