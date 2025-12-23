using System;
using System.Collections.Generic;
using Lexer.Tokens;
using Parser.AST.Declarations;
using Parser.Visitor;

namespace Parser.AST
{
    // Корневой узел абстрактного синтаксического дерева (AST).
    // Представляет собой результат парсинга всей программы.
    public class ProgramNode : AstNode
    {
        // Список объявлений верхнего уровня (Top-Level Declarations).
        // Сюда входят классы, глобальные функции и (опционально) глобальные переменные.
        public List<Declaration> Declarations { get; }

        // Тип узла - Program.
        public override AstNodeType NodeType => AstNodeType.Program;

        // Создает корневой узел программы.
        public ProgramNode(List<Declaration> declarations)
            : base(null) // У корневого узла нет одного конкретного токена (он охватывает весь файл)
        {
            Declarations = declarations ?? new List<Declaration>();
        }

        // Принимает посетителя (Visitor) для обработки всей программы.
        // Обычно это точка входа для семантического анализатора или генератора кода.
        public override T Accept<T>(IAstVisitor<T> visitor)
        {
            return visitor.VisitProgram(this);
        }
    }
}