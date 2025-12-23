using System;
using Lexer.Tokens;
using Parser.AST.Expressions;
using Parser.Visitor;

namespace Parser.AST.Declarations
{
    // Объявление переменной или поля класса.
    // Пример: int x = 10;
    public class VariableDeclaration : Declaration
    {
        // Тип переменной (например, "int", "string[]").
        public string TypeName { get; }

        public override string Name { get; }

        // Выражение инициализации (может быть null, если инициализации нет).
        public Expression Initializer { get; }
        
        // Флаг публичного доступа (для полей класса).
        public bool IsPublic { get; }

        public override AstNodeType NodeType => AstNodeType.VariableDeclaration;

        public VariableDeclaration(string typeName, string name, Expression initializer, bool isPublic = false)
            : base(new Token(TokenType.IDENTIFIER, name)) // Используем имя как основной токен
        {
            TypeName = typeName ?? throw new ArgumentNullException(nameof(typeName));
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Initializer = initializer;
            IsPublic = isPublic;
        }

        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitVariableDeclaration(this);
        }
    }
}