using System;
using Lexer.Tokens;

namespace Parser.Parser
{
    public class ParserDiagnostic
    {
        // Уровень диагностики (Ошибка, Предупреждение, Инфо).
        public ParserDiagnosticLevel Level { get; }

        // Текст сообщения об ошибке.
        public string Message { get; }

        // Токен, с которым связана проблема (может быть null, если ошибка общая).
        public Token Token { get; }

        public ParserDiagnostic(ParserDiagnosticLevel level, string message, Token token)
        {
            Level = level;
            Message = message;
            Token = token;
        }

        public override string ToString()
        {
            string location = "";
            if (Token != null)
            {
                if (Token.Type == TokenType.EOF)
                {
                    location = " at end of file";
                }
                else
                {
                    location = $" at line {Token.Line}, column {Token.Column} ('{Token.Text}')";
                }
            }

            return $"{Level}: {Message}{location}";
        }
    }
}