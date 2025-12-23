using System;
using System.Collections.Generic;
using Lexer.Tokens;
using Parser.Visitor;

namespace Parser.AST.Statements
{
    // Блок инструкций, заключенный в фигурные скобки { ... }.
    // Вводит новую лексическую область видимости.
    public class BlockStatement : Statement
    {
        // Список инструкций внутри блока.
        public List<Statement> Statements { get; }

        public override AstNodeType NodeType => AstNodeType.BlockStatement;

        public BlockStatement(List<Statement> statements)
            : base(new Token(TokenType.BRACE_OPEN, "{"))
        {
            Statements = statements ?? new List<Statement>();
        }

        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitBlockStatement(this);
        }
    }
}