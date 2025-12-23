using Lexer.Tokens;
using Parser.Visitor;

namespace Parser.AST
{
    // Базовый абстрактный класс для всех узлов AST.
    // Хранит информацию о токене (для отладки и сообщений об ошибках)
    // и поддерживает паттерн Visitor.
    public abstract class AstNode
    {
        // Тип узла (для идентификации без использования is/as).
        public abstract AstNodeType NodeType { get; }

        // Основной токен, ассоциированный с этим узлом.
        // Используется для получения позиции в коде (строка, колонка) при ошибках.
        // Например: ключевое слово 'if', имя переменной, или оператор '+'.
        public Token Token { get; }

        protected AstNode(Token token)
        {
            Token = token;
        }

        // Метод для приема посетителя (Visitor Pattern).
        // Позволяет отделить алгоритмы обхода дерева (анализ, генерация кода) от самих классов узлов.
        public abstract T Accept<T>(IAstVisitor<T> visitor);
    }
}