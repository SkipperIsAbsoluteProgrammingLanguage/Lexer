using System;
using Lexer.Tokens;
using Parser.Visitor;

namespace Parser.AST.Expressions
{
    // Узел бинарного выражения (Left Operator Right).
    // Примеры: 1 + 2, x * y, a == b, x = 5.
    public class BinaryExpression : Expression
    {
        // Левый операнд.
        public Expression Left { get; }

        // Оператор (например, +, -, *, =, ==).
        public Token Operator { get; }

        // Правый операнд.
        public Expression Right { get; }

        public override AstNodeType NodeType => AstNodeType.BinaryExpression;

        public BinaryExpression(Expression left, Token op, Expression right)
            : base(op) // В качестве основного токена узла используем оператор
        {
            Left = left ?? throw new ArgumentNullException(nameof(left));
            Operator = op ?? throw new ArgumentNullException(nameof(op));
            Right = right ?? throw new ArgumentNullException(nameof(right));
        }

        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitBinaryExpression(this);
        }
    }
}