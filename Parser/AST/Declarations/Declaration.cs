using Lexer.Tokens;
using Parser.AST.Statements;

namespace Parser.AST.Declarations
{
    // Базовый класс для всех объявлений (переменные, функции, классы).
    // Объявление вводит новый идентификатор в текущую область видимости.
    public abstract class Declaration : Statement
    {
        // Имя объявляемой сущности.
        public abstract string Name { get; }

        protected Declaration(Token token) : base(token)
        {
        }
    }
}