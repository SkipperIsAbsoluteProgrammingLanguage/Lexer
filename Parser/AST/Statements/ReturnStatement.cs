using Lexer.Tokens;
using Parser.AST.Expressions;
using Parser.Visitor;

namespace Parser.AST.Statements
{
    // Инструкция возврата из функции.
    // Пример: return x + y; или return;
    public class ReturnStatement : Statement
    {
        // Возвращаемое значение.
        // Может быть null, если функция возвращает void.
        public Expression Value { get; }

        public override AstNodeType NodeType => AstNodeType.ReturnStatement;

        public ReturnStatement(Expression value)
            : base(new Token(TokenType.KEYWORD_RETURN, "return"))
        {
            Value = value;
        }

        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitReturnStatement(this);
        }
    }
}