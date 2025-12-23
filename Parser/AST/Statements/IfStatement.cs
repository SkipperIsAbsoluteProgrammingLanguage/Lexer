using System;
using Lexer.Tokens;
using Parser.AST.Expressions;
using Parser.Visitor;

namespace Parser.AST.Statements
{
    // Условная инструкция if-else.
    public class IfStatement : Statement
    {
        // Условие выполнения (должно вычисляться в boolean).
        public Expression Condition { get; }

        // Ветка "то" (выполняется, если условие истинно).
        public Statement ThenBranch { get; }

        // Ветка "иначе" (может быть null).
        public Statement ElseBranch { get; }

        public override AstNodeType NodeType => AstNodeType.IfStatement;

        public IfStatement(Expression condition, Statement thenBranch, Statement elseBranch)
            : base(new Token(TokenType.KEYWORD_IF, "if"))
        {
            Condition = condition ?? throw new ArgumentNullException(nameof(condition));
            ThenBranch = thenBranch ?? throw new ArgumentNullException(nameof(thenBranch));
            ElseBranch = elseBranch; // Может быть null
        }

        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitIfStatement(this);
        }
    }
}