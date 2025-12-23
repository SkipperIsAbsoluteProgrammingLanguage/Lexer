using System;
using System.Collections.Generic;
using Lexer.Tokens;
using Parser.Visitor;

namespace Parser.AST.Expressions
{
    // Узел вызова функции.
    // Примеры: func(), calculate(a, b), obj.method().
    public class CallExpression : Expression
    {
        // Выражение, которое вызывается (обычно IdentifierExpression или MemberAccessExpression).
        public Expression Callee { get; }

        // Список аргументов вызова.
        public List<Expression> Arguments { get; }

        public override AstNodeType NodeType => AstNodeType.CallExpression;

        // Создает узел вызова.
        public CallExpression(Expression callee, List<Expression> arguments)
            : base(callee?.Token) // Используем токен вызываемой функции как основной
        {
            Callee = callee ?? throw new ArgumentNullException(nameof(callee));
            Arguments = arguments ?? new List<Expression>();
        }

        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitCallExpression(this);
        }
    }
}