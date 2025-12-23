namespace Parser.AST
{
    // Перечисление всех типов узлов абстрактного синтаксического дерева.
    // Используется для быстрой идентификации типа узла без reflection.
    public enum AstNodeType
    {
        // Корневой узел
        Program,

        // --- Объявления (Declarations) ---
        FunctionDeclaration,
        VariableDeclaration,
        ClassDeclaration,
        ParameterDeclaration,

        // --- Инструкции (Statements) ---
        BlockStatement,
        IfStatement,
        WhileStatement,
        ForStatement,
        ReturnStatement,
        ExpressionStatement,

        // --- Выражения (Expressions) ---
        BinaryExpression,       // a + b, a == b, a = b
        UnaryExpression,        // -a, !a
        LiteralExpression,      // 123, "text", true
        IdentifierExpression,   // myVar
        CallExpression,         // func(x)
        ArrayAccessExpression,  // arr[i]
        MemberAccessExpression, // obj.field
        NewArrayExpression,     // new int[10]
        NewObjectExpression,    // new MyClass()
        TernaryExpression       // a > b ? a : b
    }
}