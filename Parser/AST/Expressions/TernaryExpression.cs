using System;
using Lexer.Tokens;
using Parser.Visitor;

namespace Parser.AST.Expressions
{
    // Узел тернарного оператора.
    // Пример: a > b ? a : b
    public class TernaryExpression : Expression
    {
        public Expression Condition { get; }
        public Expression ThenBranch { get; }
        public Expression ElseBranch { get; }
        public override AstNodeType NodeType => AstNodeType.TernaryExpression; 

        public TernaryExpression(Expression condition, Expression thenBranch, Expression elseBranch, Token questionMark)
            : base(questionMark)
        {
            Condition = condition ?? throw new ArgumentNullException(nameof(condition));
            ThenBranch = thenBranch ?? throw new ArgumentNullException(nameof(thenBranch));
            ElseBranch = elseBranch ?? throw new ArgumentNullException(nameof(elseBranch));
        }

        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            // Требуется метод VisitTernaryExpression в интерфейсе визитора
            // return visitor.VisitTernaryExpression(this);
            throw new NotImplementedException("Visitor for TernaryExpression is not yet defined");
        }
    }
}