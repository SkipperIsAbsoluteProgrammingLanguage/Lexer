using System;
using Lexer.Tokens;
using Parser.Visitor;

namespace Parser.AST.Expressions
{
    // Узел создания нового массива.
    // Пример: new int[10].
    public class NewArrayExpression : Expression
    {
        // Имя типа элементов массива (например, "int", "string").
        public string ElementType { get; }

        // Выражение, определяющее размер массива.
        public Expression SizeExpression { get; }

        public override AstNodeType NodeType => AstNodeType.NewArrayExpression;

        public NewArrayExpression(string elementType, Expression sizeExpression)
            // Создаем синтетический токен или берем null, так как 'new' уже прошел
            : base(new Token(TokenType.KEYWORD_NEW, "new"))
        {
            ElementType = elementType ?? throw new ArgumentNullException(nameof(elementType));
            SizeExpression = sizeExpression ?? throw new ArgumentNullException(nameof(sizeExpression));
        }

        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitNewArrayExpression(this);
        }
    }
}