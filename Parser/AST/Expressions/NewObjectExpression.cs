using System;
using System.Collections.Generic;
using Lexer.Tokens;
using Parser.Visitor;

namespace Parser.AST.Expressions
{
    // Узел создания нового экземпляра класса.
    // Пример: new User("Ivan").
    public class NewObjectExpression : Expression
    {
        // Имя класса.
        public string ClassName { get; }

        // Аргументы конструктора.
        public List<Expression> Arguments { get; }

        public override AstNodeType NodeType => AstNodeType.NewObjectExpression;

        public NewObjectExpression(string className, List<Expression> arguments)
             : base(new Token(TokenType.KEYWORD_NEW, "new"))
        {
            ClassName = className ?? throw new ArgumentNullException(nameof(className));
            Arguments = arguments ?? new List<Expression>();
        }

        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitNewObjectExpression(this);
        }
    }
}