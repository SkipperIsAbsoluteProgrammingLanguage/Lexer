using Lexer.Tokens;

namespace Parser.AST.Expressions
{
    // Базовый класс для всех выражений (Expression).
    // Выражение — это конструкция кода, которая вычисляется в значение (value).
    public abstract class Expression : AstNode
    {
        protected Expression(Token token) : base(token)
        {
        }
    }
}