using System;
using Lexer.Tokens;
using Parser.Visitor;

namespace Parser.AST.Expressions
{
    // Узел литерала (константы).
    // Примеры: 123, 3.14, "hello", 'a', true, false.
    public class LiteralExpression : Expression
    {
        // Распарсенное значение литерала (int, double, string, bool, char).
        public object Value { get; }

        public override AstNodeType NodeType => AstNodeType.LiteralExpression;

        // Создает выражение литерала.
        public LiteralExpression(object value, Token token)
            : base(token)
        {
            Value = value;
        }

        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitLiteralExpression(this);
        }
    }
}