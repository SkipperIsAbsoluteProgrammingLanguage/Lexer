using System;
using Lexer.Tokens;
using Parser.Visitor;

namespace Parser.AST.Expressions
{
    // Узел доступа к члену класса (полю или методу).
    // Пример: object.field или math.pi
    public class MemberAccessExpression : Expression
    {
        // Объект, к которому происходит обращение.
        public Expression Object { get; }

        // Имя поля или метода.
        public string MemberName { get; }

        public override AstNodeType NodeType => AstNodeType.MemberAccessExpression;

        public MemberAccessExpression(Expression obj, string memberName)
            : base(obj?.Token) // Используем токен объекта как базу
        {
            Object = obj ?? throw new ArgumentNullException(nameof(obj));
            MemberName = memberName ?? throw new ArgumentNullException(nameof(memberName));
        }

        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitMemberAccessExpression(this);
        }
    }
}