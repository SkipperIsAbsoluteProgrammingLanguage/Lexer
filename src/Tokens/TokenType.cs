using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Справочник всех видов слов, которые понимает язык.
namespace Lexer.Tokens
{
    public enum TokenType
    {
        // Служебные
        EOF,        // Конец файла (End Of File)
        BAD,        // Неизвестный символ (ошибка)

        // Литералы
        NUMBER,     // Цлое число (123, 42)

        // Операторы
        PLUS,       // +
        MINUS,      // -
        STAR,       // *
        SLASH,      // /
        MODULO,     // %

        // Операторы сравнения
        EQUAL, 
        NOT_EQUAL, 
        LESS, 
        GREATER, 
        LESS_EQUAL, 
        GREATER_EQUAL,

        // Логические операторы
        AND, 
        OR, 
        NOT,

        // Присваивание
        ASSIGN,

        // Стрелка для возвращаемого типа
        ARROW,

        // Тернарный оператор
        QUESTION_MARK, 
        COLON,

        FLOAT_LITERAL,    // 3.14
        CHAR_LITERAL,     // 'a'
        STRING_LITERAL,   // "hello"
        BOOL_LITERAL,     // true/false

        SEMICOLON,        // ;
        COMMA,            // ,
        DOT,              // .
        BRACE_OPEN,       // {
        BRACE_CLOSE,      // }
        BRACKET_OPEN,     // [
        BRACKET_CLOSE,    // ]
        LPAREN,           // (
        RPAREN,           // )

        IDENTIFIER,       // Имена переменных и функций

        // Ключевые слова
        KEYWORD_FN,
        KEYWORD_INT,
        KEYWORD_FLOAT,
        KEYWORD_BOOL,
        KEYWORD_CHAR,
        KEYWORD_STRING,
        KEYWORD_RETURN,
        KEYWORD_IF,
        KEYWORD_ELSE,
        KEYWORD_WHILE,
        KEYWORD_FOR,
        KEYWORD_PUBLIC,
        KEYWORD_CLASS,
        KEYWORD_NEW,
        KEYWORD_TRUE,
        KEYWORD_FALSE,
        KEYWORD_VOID
    }
}
