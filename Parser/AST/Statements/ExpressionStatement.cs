using System;
using Parser.AST.Expressions;
using Parser.Visitor;

namespace Parser.AST.Statements
{
    // Инструкция-выражение.
    // Обертка над Expression, позволяющая использовать его как Statement.
    // Обычно используется для выражений с побочными эффектами (присваивание, вызов функции).
    public class ExpressionStatement : Statement
    {
        // Вложенное выражение.
        public Expression Expression { get; }

        public override AstNodeType NodeType => AstNodeType.ExpressionStatement;

        public ExpressionStatement(Expression expression)
            : base(expression?.Token) // Используем токен самого выражения
        {
            Expression = expression ?? throw new ArgumentNullException(nameof(expression));
        }

        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitExpressionStatement(this);
        }
    }
}