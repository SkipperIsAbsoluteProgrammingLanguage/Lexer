using System.Collections.Generic;
using System.Linq;
using Xunit;
using Lexer.Lexer;
using Parser.Parser;
using Parser.AST;

namespace ParserTests
{
    public static class TestHelpers
    {
        public static ProgramNode Parse(string source, bool expectErrors = false)
        {
            var lexer = new Lexer.Lexer.Lexer(source);
            var tokens = lexer.Tokenize();
            var parser = new Parser.Parser.Parser(tokens);
            var program = parser.Parse();

            if (!expectErrors)
            {
                Assert.False(parser.HasErrors,
                    $"Parser has errors: {string.Join(", ", parser.Diagnostics.Select(d => d.Message))}");
            }
            else
            {
                Assert.True(parser.HasErrors, "Expected parser errors, but none were found.");
            }

            return program;
        }

        // Хелпер для получения единственного выражения из source (оборачивает в fn main)
        // Полезно для теста выражений типа "1 + 2 * 3" без написания полной обвязки
        public static T ParseExpression<T>(string expressionSource) where T : Parser.AST.Expressions.Expression
        {
            var source = $"fn main() {{ var = {expressionSource}; }}";
            var program = Parse(source);

            var func = (Parser.AST.Declarations.FunctionDeclaration)program.Declarations[0];
            var stmt = (Parser.AST.Statements.ExpressionStatement)func.Body.Statements[0];
            var assignment = (Parser.AST.Expressions.BinaryExpression)stmt.Expression;

            return assignment.Right as T;
        }
    }
}