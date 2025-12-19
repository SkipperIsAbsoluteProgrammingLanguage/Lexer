using Lexer.Lexer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexer.Lexer
{
    // Диагностическое сообщение лексера.
    public class LexerDiagnostic
    {
        public LexerDiagnosticLevel Level { get; }
        public string Message { get; }
        public int Line { get; }
        public int Column { get; }

        public LexerDiagnostic(LexerDiagnosticLevel level, string message, int line, int column)
        {
            Level = level;
            Message = message;
            Line = line;
            Column = column;
        }

        public override string ToString()
        {
            var levelStr = Level switch
            {
                LexerDiagnosticLevel.Error => "Ошибка",
                LexerDiagnosticLevel.Warning => "Предупреждение",
                _ => "Информация"
            };
            return $"{levelStr}: {Message} (строка {Line}, столбец {Column})";
        }
    }

}
