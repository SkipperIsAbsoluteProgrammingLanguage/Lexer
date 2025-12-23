using System;
using Lexer.Tokens;
using Parser.AST.Expressions;
using Parser.Visitor;

namespace Parser.AST.Statements
{
    // Цикл while.
    public class WhileStatement : Statement
    {
        // Условие продолжения цикла.
        public Expression Condition { get; }

        // Тело цикла.
        public Statement Body { get; }

        public override AstNodeType NodeType => AstNodeType.WhileStatement;

        public WhileStatement(Expression condition, Statement body)
            : base(new Token(TokenType.KEYWORD_WHILE, "while"))
        {
            Condition = condition ?? throw new ArgumentNullException(nameof(condition));
            Body = body ?? throw new ArgumentNullException(nameof(body));
        }

        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitWhileStatement(this);
        }
    }
}