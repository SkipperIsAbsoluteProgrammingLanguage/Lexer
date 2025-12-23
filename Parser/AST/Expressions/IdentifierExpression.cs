using System;
using Lexer.Tokens;
using Parser.Visitor;

namespace Parser.AST.Expressions
{
    // Узел идентификатора (использование переменной).
    // Примеры: x, counter, myVariable.
    public class IdentifierExpression : Expression
    {
        // Имя идентификатора.
        public string Name => Token.Text;

        public override AstNodeType NodeType => AstNodeType.IdentifierExpression;

        public IdentifierExpression(Token token)
            : base(token)
        {
            if (token.Type != TokenType.IDENTIFIER)
            {
                throw new ArgumentException($"Expected IDENTIFIER token, got {token.Type}");
            }
        }

        // Конструктор для внутреннего использования (например, при генерации кода),
        // когда токена может не быть под рукой.
        public IdentifierExpression(string name)
            : base(new Token(TokenType.IDENTIFIER, name))
        {
        }

        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitIdentifierExpression(this);
        }
    }
}