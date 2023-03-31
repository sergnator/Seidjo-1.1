using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kompil.ASP
{
    class VarNode : ExpressionNode
    {
        

        public VarNode(Token var)
        {
            
            this.var = var;
        }
    }
}
