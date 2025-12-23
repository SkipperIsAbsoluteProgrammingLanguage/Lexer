using System;
using Lexer.Tokens;

namespace Parser.Parser
{
    // Исключение, используемое для прерывания процесса парсинга при обнаружении
    // синтаксической ошибки. Позволяет механизму "Panic Mode" перехватить управление
    // и синхронизировать состояние парсера.
    public class ParserException : Exception
    {
        // Токен, на котором произошла ошибка.
        public Token Token { get; }

        public ParserException(string message, Token token)
            : base(FormatMessage(message, token))
        {
            Token = token;
        }

        public ParserException(string message)
            : base(message)
        {
            Token = null;
        }

        private static string FormatMessage(string message, Token token)
        {
            if (token == null)
            {
                return message;
            }

            if (token.Type == TokenType.EOF)
            {
                return $"{message} at end of file";
            }

            return $"{message} at '{token.Text}' (line {token.Line}, column {token.Column})";
        }
    }
}