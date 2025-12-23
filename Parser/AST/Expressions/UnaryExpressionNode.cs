using System;
using Lexer.Tokens;
using Parser.Visitor;

namespace Parser.AST.Expressions
{
    // Узел унарного выражения (Operator Operand).
    // Примеры: -5, !flag, -x.
    public class UnaryExpression : Expression
    {
        // Унарный оператор (-, !).
        public Token Operator { get; }

        // Операнд выражения.
        public Expression Operand { get; }

        public override AstNodeType NodeType => AstNodeType.UnaryExpression;

        public UnaryExpression(Token op, Expression operand)
            : base(op) // Токен оператора является основным для этого узла
        {
            Operator = op ?? throw new ArgumentNullException(nameof(op));
            Operand = operand ?? throw new ArgumentNullException(nameof(operand));
        }

        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitUnaryExpression(this);
        }
    }
}