using System;
using Lexer.Tokens;
using Parser.Visitor;

namespace Parser.AST.Expressions
{
    // Узел доступа к элементу массива.
    // Пример: arr[5], matrix[x].
    public class ArrayAccessExpression : Expression
    {
        // Выражение, вычисляемое в массив (имя массива или другой вызов).
        public Expression Target { get; }

        // Выражение индекса.
        public Expression Index { get; }

        public override AstNodeType NodeType => AstNodeType.ArrayAccessExpression;

        public ArrayAccessExpression(Expression target, Expression index)
            : base(target?.Token)
        {
            Target = target ?? throw new ArgumentNullException(nameof(target));
            Index = index ?? throw new ArgumentNullException(nameof(index));
        }

        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitArrayAccessExpression(this);
        }
    }
}