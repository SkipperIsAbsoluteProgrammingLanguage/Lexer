using System;
using Lexer.Tokens;
using Parser.AST.Expressions;
using Parser.Visitor;

namespace Parser.AST.Statements
{
    // Цикл for.
    // Пример: for (int i = 0; i < 10; i = i + 1) { ... }
    public class ForStatement : Statement
    {
        // Инициализация цикла (обычно объявление переменной или присваивание).
        // Может быть null.
        public Statement Initializer { get; }

        // Условие выполнения итерации.
        // Может быть null (бесконечный цикл).
        public Expression Condition { get; }

        // Выражение, выполняемое после каждой итерации (инкремент).
        // Может быть null.
        public Expression Increment { get; }

        // Тело цикла.
        public Statement Body { get; }

        public override AstNodeType NodeType => AstNodeType.ForStatement;

        public ForStatement(Statement initializer, Expression condition, Expression increment, Statement body)
            : base(new Token(TokenType.KEYWORD_FOR, "for"))
        {
            Initializer = initializer; // Nullable
            Condition = condition;     // Nullable
            Increment = increment;     // Nullable
            Body = body ?? throw new ArgumentNullException(nameof(body));
        }

        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitForStatement(this);
        }
    }
}