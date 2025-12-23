using Lexer.Tokens;

namespace Parser.AST.Statements
{
    // Базовый класс для всех инструкций (Statement).
    // Инструкция — это единица выполнения кода, которая не возвращает значения.
    public abstract class Statement : AstNode
    {
        protected Statement(Token token) : base(token)
        {
        }
    }
}