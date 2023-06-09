﻿using Kompil.ASP;

namespace Kompil
{
    class Parser
    {
        public List<Token> tokens { get; set; }
        public int pos { get; set; }
        public Dictionary<string, dynamic> scope { get; set; }

        

        public Parser(List<Token> tokens)
        {
            this.tokens = tokens;
            scope = new Dictionary<string, dynamic>() { {"2", "14" } };
        }

        public dynamic? run(ExpressionNode node)
        {
            if(node is InputFunc )
            {
                return Console.ReadLine();
            }
            if (node is NumderNode)
            {
                return Convert.ToInt32(node.number.Text);
            }
            if (node is UnarOperationNode)
            {
                if (node.Operator.Type.name == "PRINT")
                {
                    Console.WriteLine(run(node.Operand));
                    return null;
                }
            }
            if (node is BinOperatorNode)
            {
                if (node.Operator.Type.name == "PLUS")
                {
                    return run(node.left) + run(node.right);

                }
                if (node.Operator.Type.name == "MINUS")
                {
                    return run(node.left) - run(node.right);
                }
                if (node.Operator.Type.name == "EQ")
                {
                    var result = run(node.right);
                    var VarNode = node.left;
                    try
                    {
                        scope.Add(VarNode.var.Text, result);
                    }
                    catch
                    {
                        scope[VarNode.var.Text] = result;
                    }
                    return result;
                }
            }
            if (node is VarNode)
            {
                try { return scope[node.var.Text]; }
                catch { throw new Exception("Неизвестная переменная"); }
                    
                
                
            }
            if (node is StatementsNode)
            {
                foreach(var i in node.codeStrings)
                {
                    run(i);
                }
                return null;
            }
            throw new Exception("неизвестный синтаксис");
        }

            public Token math(TokenType expected)
            {
                
                if (pos < tokens.Count)
                {
                while (tokens[pos].Type.name == "SPACE")
                {
                    pos++;
                }
                Token currentToken = tokens[pos];
                
                    if (expected.name == currentToken.Type.name)
                    {
                        pos++;
                        return currentToken;
                    }
            }
                return null;
            }

        public Token require(TokenType ex)
        {
            Token token = math(ex);
            if (token == null)
            {
                throw new Exception($"на позиции {pos} ожидается  {ex.name}");
            }
            return token;
        }

        public ExpressionNode parseCode()
        {
            StatementsNode root = new StatementsNode();
            while (pos < tokens.Count)
            {
                ExpressionNode codeStringNode = parseExpression();
                require(TokenType.TokenTypes["END"]);
                root.addNode(codeStringNode);
            }
            return root;
        }
        public ExpressionNode parseInput()
        {
            Token operatInput = math(TokenType.TokenTypes["INPUT"]);
            if(operatInput != null)
            {
                return new InputFunc();
            }
            throw new Exception("неверный синтаксис");
        }
        public ExpressionNode parseExpression()
        {
            if (math(TokenType.TokenTypes["VAR"]) == null)
            {
                ExpressionNode printNode = new ExpressionNode();

                try
                {
                    printNode = parsePrint();
                }
                catch  { }
                try
                {
                    printNode = parseInput();
                }
                catch { }
                try
                {
                    printNode = ParseParseInt();
                }
                catch { }
                if (math(TokenType.TokenTypes["RIGHTSKOB"]) != null)
                {
                    return printNode;
                }
                throw new Exception("не закрытая скобка после функции ВЫВОД");
            }
            
            ExpressionNode varnode = parseVarOrNum();
            Token ass = math(TokenType.TokenTypes["EQ"]);
            if (ass != null)
            {
                if (math(TokenType.TokenTypes["CONVERTTOINT"]) != null)
                {
                    
                    BinOperatorNode bin = new BinOperatorNode(ass, varnode, ParseParseInt());
                    return bin;
                }
                if (math(TokenType.TokenTypes["INPUT"]) != null)
                {
                    pos--;
                    BinOperatorNode bin = new BinOperatorNode(ass, varnode, parseInput());
                    return bin;
                }
                ExpressionNode right = parseFormula();
                BinOperatorNode binaryNode = new BinOperatorNode(ass, varnode, right);
                return binaryNode;
            }
            throw new Exception("нет оператора присвоения");

        }
        public ExpressionNode ParseParseInt()
        {
            pos--;//проблема в том, что после всех проверок ParseInput() возращает ошибку проблема в pos наверное
            Token ParseInt = math(TokenType.TokenTypes["CONVERTTOINT"]);
            if(ParseInt != null)
            {
                Token Skobkaleft = math(TokenType.TokenTypes["LEFTSKOB"]);
                if (Skobkaleft != null)
                {
                    Token Input = math(TokenType.TokenTypes["INPUT"]);
                    if(Input != null)
                    {
                        if (math(TokenType.TokenTypes["RIGHTSKOB"]) != null)
                        {
                            pos--;
                            return new UnarOperationNode(ParseInt, parseInput());
                        }
                    }
                }
            }
            throw new Exception("неверный синтаксис");
        }
        public ExpressionNode parseNameVar()
        {
            Token name = math(TokenType.TokenTypes["NAMEVAR"]);
            if(name != null)
            {

            }
            throw new Exception("нет название переменной" + pos);
        }
        public ExpressionNode parsePrint()
        {
            Token operatorLog = math(TokenType.TokenTypes["PRINT"]);
            if(operatorLog != null)
            {
                Token Skobkaleft = math(TokenType.TokenTypes["LEFTSKOB"]);
                if (Skobkaleft != null)
                {
                    return new UnarOperationNode(operatorLog, parseFormula());
                }

            }
            throw new Exception("неверный синтаксис");
        }

        public ExpressionNode parseFormula()
        {
            ExpressionNode leftNode = parseVarOrNum();
            Token Operator = math(TokenType.TokenTypes["PLUS"]);
            if(Operator == null)
            {
                Operator = math(TokenType.TokenTypes["MINUS"]);
            }
            while(Operator != null)
            {
                ExpressionNode rightNode = parseVarOrNum();
                leftNode = new BinOperatorNode(Operator, leftNode, rightNode);
                Operator = math(TokenType.TokenTypes["PLUS"]);
                if (Operator == null)
                {
                    Operator = math(TokenType.TokenTypes["MINUS"]);
                }

            }
            return leftNode;
        }

        public ExpressionNode parseVarOrNum()
        {
            Token num = math(TokenType.TokenTypes["NUM"]);
            if(num != null)
            {
                return new NumderNode(num);
            }
            Token var = math(TokenType.TokenTypes["NAMEVAR"]);
            if(var != null)
            {
                return new VarNode(var);
            }
            throw new Exception("неверный синтаксис");
        }

    }
}
