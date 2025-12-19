using Xunit;
using Lexer.Tokens;

namespace LexerTests
{
    public class TokenTests
    {
        [Fact]
        public void Token_Constructor_SetsProperties()
        {
            // Arrange & Act
            var token = new Token(TokenType.NUMBER, "123", 10, 2, 5);

            // Assert
            Assert.Equal(TokenType.NUMBER, token.Type);
            Assert.Equal("123", token.Text);
            Assert.Equal(10, token.StartPosition);
            Assert.Equal(2, token.Line);
            Assert.Equal(5, token.Column);
            Assert.Equal(3, token.Length);
            Assert.Equal(13, token.EndPosition);
        }

        [Fact]
        public void Token_IsKeyword_ReturnsTrueForKeywords()
        {
            // Arrange
            var keywordToken = new Token(TokenType.KEYWORD_FN, "fn", 0, 1, 1);
            var nonKeywordToken = new Token(TokenType.IDENTIFIER, "x", 0, 1, 1);

            // Act & Assert
            Assert.True(keywordToken.IsKeyword);
            Assert.False(nonKeywordToken.IsKeyword);
        }

        [Fact]
        public void Token_IsLiteral_ReturnsTrueForLiterals()
        {
            // Arrange
            var numberToken = new Token(TokenType.NUMBER, "123", 0, 1, 1);
            var stringToken = new Token(TokenType.STRING_LITERAL, "\"hello\"", 0, 1, 1);
            var nonLiteralToken = new Token(TokenType.IDENTIFIER, "x", 0, 1, 1);

            // Act & Assert
            Assert.True(numberToken.IsLiteral);
            Assert.True(stringToken.IsLiteral);
            Assert.False(nonLiteralToken.IsLiteral);
        }

        [Fact]
        public void Token_GetNumericValue_ReturnsCorrectValue()
        {
            // Arrange
            var intToken = new Token(TokenType.NUMBER, "42", 0, 1, 1);
            var floatToken = new Token(TokenType.FLOAT_LITERAL, "3.14", 0, 1, 1);

            // Act
            var intValue = intToken.GetNumericValue();
            var floatValue = floatToken.GetNumericValue();

            // Assert
            Assert.Equal(42L, intValue);
            Assert.Equal(3.14, (double)floatValue, 5);
        }

        [Fact]
        public void Token_Equals_ComparesTypeAndText()
        {
            // Arrange
            var token1 = new Token(TokenType.NUMBER, "123", 0, 1, 1);
            var token2 = new Token(TokenType.NUMBER, "123", 10, 2, 5); // другая позиция
            var token3 = new Token(TokenType.NUMBER, "456", 0, 1, 1); // другой текст
            var token4 = new Token(TokenType.IDENTIFIER, "123", 0, 1, 1); // другой тип

            // Act & Assert
            Assert.Equal(token1, token2); // Только тип и текст
            Assert.NotEqual(token1, token3);
            Assert.NotEqual(token1, token4);
        }

        [Fact]
        public void Token_ToString_ReturnsFormattedString()
        {
            // Arrange
            var token = new Token(TokenType.NUMBER, "123", 10, 2, 5);

            // Act
            var result = token.ToString();

            // Assert
            Assert.Contains("Token(", result);
            Assert.Contains("NUMBER", result);
            Assert.Contains("'123'", result);
            Assert.Contains("2:5", result); // строка:столбец
        }
    }
}