using System;

namespace Lexer.Tokens
{

    // Контейнер для токена - единицы лексического анализа.
    // Содержит тип токена, его текст и позицию в исходном коде.
    public class Token
    {

        // Тип токена (например, NUMBER, IDENTIFIER)
        public TokenType Type { get; }


        // Исходный текст токена (например, "123", "+", "fn")
        public string Text { get; }


        // Позиция начала токена в исходной строке (нумерация с 0)
        public int StartPosition { get; }


        // Длина токена в символах
        public int Length => Text?.Length ?? 0;


        // Позиция конца токена (исключающая)
        public int EndPosition => StartPosition + Length;


        // Номер строки в исходном коде (начинается с 1)
        public int Line { get; }


        // Номер столбца в строке (начинается с 1)
        public int Column { get; }


        // Создает новый токен
        public Token(TokenType type, string text, int startPosition, int line, int column)
        {
            Type = type;
            Text = text ?? throw new ArgumentNullException(nameof(text));
            StartPosition = startPosition;
            Line = line;
            Column = column;
        }


        // Создает токен с позицией по умолчанию (для тестов)
        public Token(TokenType type, string text)
            : this(type, text, 0, 1, 1)
        {
        }


        // Проверяет, является ли токен указанного типа
        public bool Is(TokenType type) => Type == type;


        // Проверяет, является ли токен любым из указанных типов
        public bool IsAny(params TokenType[] types) => Array.Exists(types, t => t == Type);


        // Проверяет, является ли токен ключевым словом
        public bool IsKeyword => Type.ToString().StartsWith("KEYWORD_");


        // Проверяет, является ли токен литералом
        public bool IsLiteral => Type == TokenType.NUMBER ||
                                 Type == TokenType.FLOAT_LITERAL ||
                                 Type == TokenType.CHAR_LITERAL ||
                                 Type == TokenType.STRING_LITERAL ||
                                 Type == TokenType.BOOL_LITERAL;


        // Проверяет, является ли токен оператором
        public bool IsOperator => Type == TokenType.PLUS ||
                                  Type == TokenType.MINUS ||
                                  Type == TokenType.STAR ||
                                  Type == TokenType.SLASH ||
                                  Type == TokenType.ASSIGN ||
                                  Type == TokenType.EQUAL ||
                                  Type == TokenType.NOT_EQUAL ||
                                  Type == TokenType.LESS ||
                                  Type == TokenType.GREATER ||
                                  Type == TokenType.LESS_EQUAL ||
                                  Type == TokenType.GREATER_EQUAL ||
                                  Type == TokenType.AND ||
                                  Type == TokenType.OR ||
                                  Type == TokenType.NOT;


        // Получает числовое значение для числовых токенов
        public object GetNumericValue()
        {
            if (Type == TokenType.NUMBER)
            {
                if (long.TryParse(Text, out long intValue))
                    return intValue;
            }
            else if (Type == TokenType.FLOAT_LITERAL)
            {
                if (double.TryParse(Text, System.Globalization.NumberStyles.Float,
                    System.Globalization.CultureInfo.InvariantCulture, out double floatValue))
                    return floatValue;
            }

            throw new InvalidOperationException($"Token {Type} is not a numeric literal");
        }


        // Получает значение bool для булевых токенов
        public bool GetBoolValue()
        {
            if (Type != TokenType.BOOL_LITERAL)
                throw new InvalidOperationException($"Token {Type} is not a boolean literal");

            return Text == "true";
        }


        // Получает строковое значение для строковых и символьных токенов (с обработкой escape-последовательностей)
        public string GetStringValue()
        {
            if (Type != TokenType.STRING_LITERAL && Type != TokenType.CHAR_LITERAL)
                throw new InvalidOperationException($"Token {Type} is not a string or character literal");

            // Убираем кавычки
            var content = Text;
            if (Type == TokenType.STRING_LITERAL && content.Length >= 2)
                content = content.Substring(1, content.Length - 2);
            else if (Type == TokenType.CHAR_LITERAL && content.Length >= 2)
                content = content.Substring(1, content.Length - 2);

            // Обработка escape-последовательностей будет в лексере
            return content;
        }


        // Форматирует позицию токена для вывода ошибок
        public string FormatPosition()
        {
            return $"(строка {Line}, столбец {Column})";
        }


        // Создает копию токена с новым типом (полезно для преобразования IDENTIFIER в KEYWORD)
        public Token WithType(TokenType newType)
        {
            return new Token(newType, Text, StartPosition, Line, Column);
        }


        // Переопределение ToString для удобства отладки
        public override string ToString()
        {
            return $"Token({Type}, '{Text}' at {Line}:{Column})";
        }


        // Проверка равенства (только по типу и тексту, без учета позиции)
        public override bool Equals(object obj)
        {
            return obj is Token other &&
                   Type == other.Type &&
                   Text == other.Text;
        }


        // Хеш-код (только тип и текст)
        public override int GetHashCode()
        {
            return HashCode.Combine(Type, Text);
        }


        // Оператор равенства для удобства
        public static bool operator ==(Token left, Token right)
        {
            if (ReferenceEquals(left, right))
                return true;
            if (left is null || right is null)
                return false;
            return left.Equals(right);
        }


        // Оператор неравенства для удобства
        public static bool operator !=(Token left, Token right)
        {
            return !(left == right);
        }
    }
}