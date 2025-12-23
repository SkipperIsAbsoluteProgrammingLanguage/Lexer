using System;
using System.Collections.Generic;
using Lexer.Tokens;
using Parser.AST.Statements;
using Parser.Visitor;

namespace Parser.AST.Declarations
{
    // Объявление функции или метода.
    // Содержит сигнатуру (имя, параметры, возвращаемый тип) и тело.
    public class FunctionDeclaration : Declaration
    {
        public override string Name { get; }

        // Возвращаемый тип функции (например, "int", "void").
        public string ReturnType { get; }

        // Список параметров.
        public List<ParameterDeclaration> Parameters { get; }

        // Тело функции (блок кода).
        public BlockStatement Body { get; }

        // Флаг публичного доступа.
        public bool IsPublic { get; }

        public override AstNodeType NodeType => AstNodeType.FunctionDeclaration;

        public FunctionDeclaration(string name, string returnType, List<ParameterDeclaration> parameters, BlockStatement body, bool isPublic)
            : base(new Token(TokenType.KEYWORD_FN, "fn")) // Или токен имени, зависит от предпочтений для отладки
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            ReturnType = returnType ?? throw new ArgumentNullException(nameof(returnType));
            Parameters = parameters ?? new List<ParameterDeclaration>();
            Body = body ?? throw new ArgumentNullException(nameof(body));
            IsPublic = isPublic;
        }

        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitFunctionDeclaration(this);
        }
    }
}