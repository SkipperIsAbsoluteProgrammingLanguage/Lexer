using System;
using System.Collections.Generic;
using Lexer.Tokens;
using Parser.Visitor;

namespace Parser.AST.Declarations
{
    // Объявление класса.
    // Класс является контейнером для полей и методов.
    public class ClassDeclaration : Declaration
    {
        public override string Name { get; }

        // Члены класса (поля и методы).
        public List<Declaration> Members { get; }

        public override AstNodeType NodeType => AstNodeType.ClassDeclaration;

        public ClassDeclaration(string name, List<Declaration> members)
            : base(new Token(TokenType.KEYWORD_CLASS, "class"))
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Members = members ?? new List<Declaration>();
        }

        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitClassDeclaration(this);
        }
    }
}