using Lexer.Lexer;
using Lexer.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lexer.Lexer
{
    // Результат работы лексера.
    public class LexerResult
    {
        public List<Token> Tokens { get; }
        public List<LexerDiagnostic> Diagnostics { get; }
        public bool HasErrors => Diagnostics.Exists(d => d.Level == LexerDiagnosticLevel.Error);

        public LexerResult(List<Token> tokens, List<LexerDiagnostic> diagnostics)
        {
            Tokens = tokens;
            Diagnostics = diagnostics;
        }
    }
}
