namespace Compiler.CodeAnalysis;

using Enums;

internal class Lexer(string text)
{
    private readonly List<string> _diagnostics = [];
    private int _position;

    public IEnumerable<string> Diagnostics => _diagnostics;

    private char Current => _position >= text.Length ? '\0' : text[_position];

    public SyntaxToken Lex()
    {
        if (_position >= text.Length)
        {
            return new SyntaxToken(SyntaxKind.EndOfFileToken, _position, "\0");
        }

        if (char.IsDigit(Current))
        {
            var start = _position;

            while (char.IsDigit(Current))
            {
                Next();
            }

            var tokenText = text[start.._position];

            if (!int.TryParse(tokenText, out var value))
            {
                _diagnostics.Add($"The number {text} isn't valid Int32.");
            }

            return new SyntaxToken(SyntaxKind.NumberToken, start, tokenText, value);
        }

        if (char.IsWhiteSpace(Current))
        {
            var start = _position;

            while (char.IsWhiteSpace(Current))
            {
                Next();
            }

            var tokenText = text[start.._position];

            return new SyntaxToken(SyntaxKind.WhitespaceToken, start, tokenText);
        }

        if (char.IsLetter(Current))
        {
            var start = _position;

            while (char.IsLetter(Current))
            {
                Next();
            }

            var tokenText = text[start.._position];

            var kind = SyntaxFacts.GetKeywordKind(tokenText);

            return new SyntaxToken(kind, start, tokenText);
        }

        switch (Current)
        {
            case '+':
                return new SyntaxToken(SyntaxKind.PlusToken, _position++, "+");
            case '-':
                return new SyntaxToken(SyntaxKind.MinusToken, _position++, "-");
            case '*':
                return new SyntaxToken(SyntaxKind.StarToken, _position++, "*");
            case '/':
                return new SyntaxToken(SyntaxKind.SlashToken, _position++, "/");
            case '(':
                return new SyntaxToken(SyntaxKind.OpenParenthesisToken, _position++, "(");
            case ')':
                return new SyntaxToken(SyntaxKind.CloseParenthesisToken, _position++, ")");
            default:
                _diagnostics.Add($"ERROR: bad character input: '{Current}'");
                return new SyntaxToken(SyntaxKind.BadToken, _position++, text.Substring(_position - 1, 1));
        }
    }

    private void Next()
    {
        _position++;
    }
}