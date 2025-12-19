using System.Collections.Generic;
using System.Linq;
using Lexer.Tokens;

namespace LexerTests
{
    public static class TestHelpers
    {
        public static List<Token> TokenizeString(string input)
        {
            var lexer = new Lexer.Lexer.Lexer(input);
            return lexer.Tokenize();
        }

        public static List<TokenType> GetTokenTypes(string input)
        {
            var tokens = TokenizeString(input);
            return tokens.Select(t => t.Type).ToList();
        }

        public static List<string> GetTokenTexts(string input)
        {
            var tokens = TokenizeString(input);
            return tokens.Select(t => t.Text).ToList();
        }

        public static void AssertTokenSequence(string input, params TokenType[] expectedTypes)
        {
            var actualTypes = GetTokenTypes(input);

            // Игнорируем EOF для упрощения
            actualTypes = actualTypes.Where(t => t != TokenType.EOF).ToList();

            Assert.Equal(expectedTypes.Length, actualTypes.Count);

            for (int i = 0; i < expectedTypes.Length; i++)
            {
                Assert.Equal(expectedTypes[i], actualTypes[i]);
            }
        }
    }
}