using System;
using Lexer.Tokens;
using Parser.Visitor;

namespace Parser.AST.Declarations
{
    // Объявление параметра функции.
    // Пример: (int count).
    public class ParameterDeclaration : Declaration
    {
        public string TypeName { get; }
        public override string Name { get; }

        public override AstNodeType NodeType => AstNodeType.ParameterDeclaration;

        public ParameterDeclaration(string typeName, string name)
            : base(new Token(TokenType.IDENTIFIER, name))
        {
            TypeName = typeName ?? throw new ArgumentNullException(nameof(typeName));
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitParameterDeclaration(this);
        }
    }
}