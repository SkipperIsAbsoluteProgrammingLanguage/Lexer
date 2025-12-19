using System;
using System.Collections.Generic;
using System.Text;
using Lexer.Tokens;

namespace Lexer.Lexer
{
    
    // Лексический анализатор (лексер) - преобразует исходный код в список токенов.
    public class Lexer
    {
        private readonly string _source;
        private int _position;
        private int _line = 1;
        private int _column = 1;
        private readonly StringBuilder _tokenBuilder = new StringBuilder();
        private readonly List<Token> _tokens = new List<Token>();

        // Словарь ключевых слов
        private static readonly Dictionary<string, TokenType> Keywords = new Dictionary<string, TokenType>
        {
            // Типы
            { "int", TokenType.KEYWORD_INT },
            { "float", TokenType.KEYWORD_FLOAT },
            { "bool", TokenType.KEYWORD_BOOL },
            { "char", TokenType.KEYWORD_CHAR },
            { "string", TokenType.KEYWORD_STRING },
            { "void", TokenType.KEYWORD_VOID },
            
            // Управляющие конструкции
            { "fn", TokenType.KEYWORD_FN },
            { "return", TokenType.KEYWORD_RETURN },
            { "if", TokenType.KEYWORD_IF },
            { "else", TokenType.KEYWORD_ELSE },
            { "while", TokenType.KEYWORD_WHILE },
            { "for", TokenType.KEYWORD_FOR },
            
            // Модификаторы и классы
            { "public", TokenType.KEYWORD_PUBLIC },
            { "class", TokenType.KEYWORD_CLASS },
            { "new", TokenType.KEYWORD_NEW },
            
            // Литералы (будут обработаны как BOOL_LITERAL, но нужны в словаре для различения)
            { "true", TokenType.BOOL_LITERAL },
            { "false", TokenType.BOOL_LITERAL }
        };

        
        // Создает новый экземпляр лексера для указанного исходного кода.
        public Lexer(string source)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
            _position = 0;
        }

        
        // Текущий символ в позиции курсора.
        private char Current => _position < _source.Length ? _source[_position] : '\0';

        
        // Следующий символ после текущего (без перемещения курсора).
        private char LookAhead => _position + 1 < _source.Length ? _source[_position + 1] : '\0';

        
        // Символ на указанном смещении от текущей позиции.
        private char Peek(int offset)
        {
            var pos = _position + offset;
            return pos < _source.Length ? _source[pos] : '\0';
        }

        
        // Перемещает курсор на один символ вперед.
        private void Advance()
        {
            if (Current == '\n')
            {
                _line++;
                _column = 1;
            }
            else
            {
                _column++;
            }
            _position++;
        }

        
        // Пропускает символ, если он совпадает с ожидаемым.
        private bool Match(char expected)
        {
            if (Current != expected)
                return false;
            Advance();
            return true;
        }

        
        // Создает токен с текущей позицией.
        private Token CreateToken(TokenType type, string text)
        {
            var startPos = _position - text.Length;
            return new Token(type, text, startPos, _line, _column - text.Length);
        }

        
        // Создает токен с указанным текстом.
        private Token CreateToken(TokenType type, StringBuilder textBuilder)
        {
            var text = textBuilder.ToString();
            return CreateToken(type, text);
        }

        
        // Пропускает пробельные символы.
        private void SkipWhitespace()
        {
            while (char.IsWhiteSpace(Current))
            {
                Advance();
            }
        }

        
        // Пропускает однострочный комментарий (// ...).
        private void SkipLineComment()
        {
            // Уже считали '//'
            while (Current != '\0' && Current != '\n')
            {
                Advance();
            }
        }

        
        // Пропускает многострочный комментарий (/* ... */).
        private void SkipBlockComment()
        {
            // Уже считали '/*'
            while (Current != '\0')
            {
                if (Current == '*' && LookAhead == '/')
                {
                    Advance(); // *
                    Advance(); // /
                    return;
                }
                Advance();
            }

            throw new LexerException("Незакрытый блочный комментарий", _line, _column);
        }

        
        // Распознает числовой литерал (целый или с плавающей точкой).
        private Token ReadNumber()
        {
            var startLine = _line;
            var startColumn = _column;
            var startPos = _position;
            _tokenBuilder.Clear();
            bool isFloat = false;

            // Целая часть
            while (char.IsDigit(Current))
            {
                _tokenBuilder.Append(Current);
                Advance();
            }

            // Дробная часть
            if (Current == '.')
            {
                isFloat = true;
                _tokenBuilder.Append(Current);
                Advance();

                // После точки должна быть хотя бы одна цифра
                if (!char.IsDigit(Current))
                {
                    throw new LexerException("Ожидалась цифра после точки в числе", _line, _column);
                }

                while (char.IsDigit(Current))
                {
                    _tokenBuilder.Append(Current);
                    Advance();
                }
            }

            // Экспоненциальная часть (опционально)
            if (Current == 'e' || Current == 'E')
            {
                isFloat = true;
                _tokenBuilder.Append(Current);
                Advance();

                // Знак экспоненты
                if (Current == '+' || Current == '-')
                {
                    _tokenBuilder.Append(Current);
                    Advance();
                }

                // Цифры экспоненты
                if (!char.IsDigit(Current))
                {
                    throw new LexerException("Ожидалась цифра в экспоненте", _line, _column);
                }

                while (char.IsDigit(Current))
                {
                    _tokenBuilder.Append(Current);
                    Advance();
                }
            }

            var tokenType = isFloat ? TokenType.FLOAT_LITERAL : TokenType.NUMBER;
            var text = _tokenBuilder.ToString();
            return new Token(tokenType, text, startPos, startLine, startColumn);
        }

        
        // Распознает строковый литерал.
        private Token ReadString()
        {
            var startLine = _line;
            var startColumn = _column;
            var startPos = _position;
            _tokenBuilder.Clear();

            var quoteChar = Current; // " или '
            var isCharLiteral = quoteChar == '\'';
            Advance(); // Пропускаем открывающую кавычку

            while (Current != '\0' && Current != quoteChar)
            {
                if (Current == '\\')
                {
                    Advance(); // Пропускаем обратную косую черту
                    _tokenBuilder.Append(ReadEscapeSequence());
                }
                else
                {
                    _tokenBuilder.Append(Current);
                    Advance();
                }
            }

            if (Current != quoteChar)
            {
                throw new LexerException("Незакрытый строковый литерал", startLine, startColumn);
            }

            Advance(); // Пропускаем закрывающую кавычку

            var text = quoteChar + _tokenBuilder.ToString() + quoteChar;
            var tokenType = isCharLiteral ? TokenType.CHAR_LITERAL : TokenType.STRING_LITERAL;

            return new Token(tokenType, text, startPos, startLine, startColumn);
        }

        
        // Читает escape-последовательность.
        private char ReadEscapeSequence()
        {
            if (Current == '\0')
            {
                throw new LexerException("Незавершенная escape-последовательность", _line, _column);
            }

            var result = Current switch
            {
                'n' => '\n',
                'r' => '\r',
                't' => '\t',
                '\\' => '\\',
                '\'' => '\'',
                '"' => '"',
                '0' => '\0',
                _ => throw new LexerException($"Неизвестная escape-последовательность: \\{Current}", _line, _column)
            };

            Advance();
            return result;
        }

        
        // Распознает идентификатор или ключевое слово.
        private Token ReadIdentifier()
        {
            var startLine = _line;
            var startColumn = _column;
            var startPos = _position;
            _tokenBuilder.Clear();

            // Первый символ может быть буквой или подчеркиванием
            _tokenBuilder.Append(Current);
            Advance();

            // Последующие символы могут быть буквами, цифрами или подчеркиванием
            while (char.IsLetterOrDigit(Current) || Current == '_')
            {
                _tokenBuilder.Append(Current);
                Advance();
            }

            var text = _tokenBuilder.ToString();

            // Проверяем, является ли это ключевым словом
            if (Keywords.TryGetValue(text, out var keywordType))
            {
                return new Token(keywordType, text, startPos, startLine, startColumn);
            }

            return new Token(TokenType.IDENTIFIER, text, startPos, startLine, startColumn);
        }

        
        // Распознает операторы и другие символы.
        private Token ReadOperatorOrPunctuation()
        {
            var startLine = _line;
            var startColumn = _column;
            var startPos = _position;

            switch (Current)
            {
                // Операторы сравнения и присваивания
                case '=':
                    Advance();
                    if (Match('='))
                        return new Token(TokenType.EQUAL, "==", startPos, startLine, startColumn);
                    return new Token(TokenType.ASSIGN, "=", startPos, startLine, startColumn);

                case '!':
                    Advance();
                    if (Match('='))
                        return new Token(TokenType.NOT_EQUAL, "!=", startPos, startLine, startColumn);
                    return new Token(TokenType.NOT, "!", startPos, startLine, startColumn);

                case '<':
                    Advance();
                    if (Match('='))
                        return new Token(TokenType.LESS_EQUAL, "<=", startPos, startLine, startColumn);
                    return new Token(TokenType.LESS, "<", startPos, startLine, startColumn);

                case '>':
                    Advance();
                    if (Match('='))
                        return new Token(TokenType.GREATER_EQUAL, ">=", startPos, startLine, startColumn);
                    return new Token(TokenType.GREATER, ">", startPos, startLine, startColumn);

                // Логические операторы
                case '&':
                    Advance();
                    if (!Match('&'))
                        throw new LexerException("Ожидался '&'", _line, _column);
                    return new Token(TokenType.AND, "&&", startPos, startLine, startColumn);

                case '|':
                    Advance();
                    if (!Match('|'))
                        throw new LexerException("Ожидался '|'", _line, _column);
                    return new Token(TokenType.OR, "||", startPos, startLine, startColumn);

                // Стрелка
                case '-':
                    Advance();
                    if (Match('>'))
                        return new Token(TokenType.ARROW, "->", startPos, startLine, startColumn);
                    return new Token(TokenType.MINUS, "-", startPos, startLine, startColumn);

                // Простые операторы и пунктуация
                case '+':
                    Advance();
                    return new Token(TokenType.PLUS, "+", startPos, startLine, startColumn);
                case '*':
                    Advance();
                    return new Token(TokenType.STAR, "*", startPos, startLine, startColumn);
                case '/':
                    Advance();
                    return new Token(TokenType.SLASH, "/", startPos, startLine, startColumn);
                case '%':
                    Advance();
                    return new Token(TokenType.MODULO, "%", startPos, startLine, startColumn);

                // Пунктуация
                case '(':
                    Advance();
                    return new Token(TokenType.LPAREN, "(", startPos, startLine, startColumn);
                case ')':
                    Advance();
                    return new Token(TokenType.RPAREN, ")", startPos, startLine, startColumn);
                case '{':
                    Advance();
                    return new Token(TokenType.BRACE_OPEN, "{", startPos, startLine, startColumn);
                case '}':
                    Advance();
                    return new Token(TokenType.BRACE_CLOSE, "}", startPos, startLine, startColumn);
                case '[':
                    Advance();
                    return new Token(TokenType.BRACKET_OPEN, "[", startPos, startLine, startColumn);
                case ']':
                    Advance();
                    return new Token(TokenType.BRACKET_CLOSE, "]", startPos, startLine, startColumn);
                case ';':
                    Advance();
                    return new Token(TokenType.SEMICOLON, ";", startPos, startLine, startColumn);
                case ',':
                    Advance();
                    return new Token(TokenType.COMMA, ",", startPos, startLine, startColumn);
                case '.':
                    Advance();
                    return new Token(TokenType.DOT, ".", startPos, startLine, startColumn);
                case '?':
                    Advance();
                    return new Token(TokenType.QUESTION_MARK, "?", startPos, startLine, startColumn);
                case ':':
                    Advance();
                    return new Token(TokenType.COLON, ":", startPos, startLine, startColumn);

                default:
                    var badChar = Current.ToString();
                    Advance();
                    return new Token(TokenType.BAD, badChar, startPos, startLine, startColumn);
            }
        }

        
        // Читает следующий токен из исходного кода.
        private Token ReadNextToken()
        {
            SkipWhitespace();

            if (Current == '\0')
                return null;

            // Комментарии
            if (Current == '/' && LookAhead == '/')
            {
                Advance(); // /
                Advance(); // /
                SkipLineComment();
                return ReadNextToken(); // Рекурсивно читаем следующий токен
            }

            if (Current == '/' && LookAhead == '*')
            {
                Advance(); // /
                Advance(); // *
                SkipBlockComment();
                return ReadNextToken(); // Рекурсивно читаем следующий токен
            }

            // Числа
            if (char.IsDigit(Current))
            {
                return ReadNumber();
            }

            // Идентификаторы и ключевые слова
            if (char.IsLetter(Current) || Current == '_')
            {
                return ReadIdentifier();
            }

            // Строки и символы
            if (Current == '"' || Current == '\'')
            {
                return ReadString();
            }

            // Операторы и пунктуация
            return ReadOperatorOrPunctuation();
        }

        
        // Преобразует исходный код в список токенов.
        public List<Token> Tokenize()
        {
            _tokens.Clear();

            while (true)
            {
                var token = ReadNextToken();
                if (token == null)
                    break;
                _tokens.Add(token);
            }

            // Добавляем токен конца файла
            _tokens.Add(new Token(TokenType.EOF, "", _position, _line, _column));

            return _tokens;
        }

        
        // Преобразует исходный код в список токенов с обработкой ошибок.
        public LexerResult TokenizeWithDiagnostics()
        {
            var tokens = new List<Token>();
            var diagnostics = new List<LexerDiagnostic>();

            var savedPosition = _position;
            var savedLine = _line;
            var savedColumn = _column;

            try
            {
                while (true)
                {
                    try
                    {
                        var token = ReadNextToken();
                        if (token == null)
                            break;

                        // Не добавляем BAD токены в список, а создаем диагностику
                        if (token.Type == TokenType.BAD)
                        {
                            diagnostics.Add(new LexerDiagnostic(
                                LexerDiagnosticLevel.Error,
                                $"Неизвестный символ '{token.Text}'",
                                token.Line,
                                token.Column
                            ));
                        }
                        else
                        {
                            tokens.Add(token);
                        }
                    }
                    catch (LexerException ex)
                    {
                        diagnostics.Add(new LexerDiagnostic(
                            LexerDiagnosticLevel.Error,
                            ex.Message,
                            ex.Line,
                            ex.Column
                        ));
                        // Пытаемся восстановиться
                        while (Current != '\0' && !char.IsWhiteSpace(Current) &&
                               !char.IsLetterOrDigit(Current) && Current != '"' && Current != '\'')
                        {
                            Advance();
                        }
                    }
                }

                tokens.Add(new Token(TokenType.EOF, "", _position, _line, _column));
            }
            finally
            {
                // Восстанавливаем состояние для возможного повторного использования
                _position = savedPosition;
                _line = savedLine;
                _column = savedColumn;
            }

            return new LexerResult(tokens, diagnostics);
        }
    }
}