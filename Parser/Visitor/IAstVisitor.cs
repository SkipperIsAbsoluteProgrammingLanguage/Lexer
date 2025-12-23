using Parser.AST;
using Parser.AST.Declarations;
using Parser.AST.Expressions;
using Parser.AST.Statements;

namespace Parser.Visitor
{
    // Интерфейс посетителя AST (Visitor Pattern).
    // Позволяет реализовать операции над деревом (интерпретация, компиляция, принтинг)
    // без изменения классов узлов.
    public interface IAstVisitor<T>
    {
        // --- Root ---
        T VisitProgram(ProgramNode node);

        // --- Declarations ---
        T VisitFunctionDeclaration(FunctionDeclaration node);
        T VisitVariableDeclaration(VariableDeclaration node);
        T VisitClassDeclaration(ClassDeclaration node);
        T VisitParameterDeclaration(ParameterDeclaration node);

        // --- Statements ---
        T VisitBlockStatement(BlockStatement node);
        T VisitIfStatement(IfStatement node);
        T VisitWhileStatement(WhileStatement node);
        T VisitForStatement(ForStatement node);
        T VisitReturnStatement(ReturnStatement node);
        T VisitExpressionStatement(ExpressionStatement node);

        // --- Expressions ---
        T VisitBinaryExpression(BinaryExpression node);
        T VisitUnaryExpression(UnaryExpression node);
        T VisitLiteralExpression(LiteralExpression node);
        T VisitIdentifierExpression(IdentifierExpression node);
        T VisitCallExpression(CallExpression node);
        T VisitTernaryExpression(TernaryExpression node);

        // Доступ к членам и массивам
        T VisitArrayAccessExpression(ArrayAccessExpression node);
        T VisitMemberAccessExpression(MemberAccessExpression node);

        // Создание объектов
        T VisitNewArrayExpression(NewArrayExpression node);
        T VisitNewObjectExpression(NewObjectExpression node);
    }
}