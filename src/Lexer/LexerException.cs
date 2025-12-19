using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexer.Lexer
{
    // Исключение лексера с информацией о позиции.
    public class LexerException : Exception
    {
        public int Line { get; }
        public int Column { get; }

        public LexerException(string message, int line, int column)
            : base($"{message} на строке {line}, столбец {column}")
        {
            Line = line;
            Column = column;
        }
    }
}
