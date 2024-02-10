namespace Compiler.CodeAnalysis;

using Enums;

internal class Lexer(string text)
{
    private int _position;

    public DiagnosticBag Diagnostics { get; } = [];

    private char Current => Peek(0);

    private char Lookahead => Peek(1);

    public SyntaxToken Lex()
    {
        if (_position >= text.Length)
        {
            return new SyntaxToken(SyntaxKind.EndOfFileToken, _position, "\0");
        }

        var start = _position;

        if (char.IsDigit(Current))
        {
            while (char.IsDigit(Current))
            {
                Next();
            }

            var tokenText = text[start.._position];

            if (!int.TryParse(tokenText, out var value))
            {
                Diagnostics.ReportInvalidNumber(
                    new TextSpan(start, _position - start),
                    text,
                    typeof(int));
            }

            return new SyntaxToken(SyntaxKind.NumberToken, start, tokenText, value);
        }

        if (char.IsWhiteSpace(Current))
        {
            while (char.IsWhiteSpace(Current))
            {
                Next();
            }

            var tokenText = text[start.._position];

            return new SyntaxToken(SyntaxKind.WhitespaceToken, start, tokenText);
        }

        if (char.IsLetter(Current))
        {
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
            case '!' when Lookahead != '=':
                return new SyntaxToken(SyntaxKind.BangToken, _position++, "!");
            case '=' when Lookahead != '=':
                return new SyntaxToken(SyntaxKind.EqualsToken, _position++, "=");
            case '&' when Lookahead == '&':
                _position += 2;
                return new SyntaxToken(SyntaxKind.AmpersandAmpersandToken, start, "&&");
            case '|' when Lookahead == '|':
                _position += 2;
                return new SyntaxToken(SyntaxKind.PipePipeToken, start, "||");
            case '=' when Lookahead == '=':
                _position += 2;
                return new SyntaxToken(SyntaxKind.EqualsEqualsToken, start, "||");
            case '!' when Lookahead == '=':
                _position += 2;
                return new SyntaxToken(SyntaxKind.BangEqualsToken, start, "||");
            default:
                Diagnostics.ReportBadCharacter(new TextSpan(_position, 1), Current);
                return new SyntaxToken(SyntaxKind.BadToken, _position++, text.Substring(_position - 1, 1));
        }
    }

    private char Peek(int offset)
    {
        return _position + offset >= text.Length ? '\0' : text[_position + offset];
    }

    private void Next()
    {
        _position++;
    }
}
