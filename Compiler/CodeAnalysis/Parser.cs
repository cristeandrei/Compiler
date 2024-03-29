﻿namespace Compiler.CodeAnalysis;

using Enums;

internal class Parser
{
    private readonly SyntaxToken[] _tokens;
    private int _position;

    public Parser(string text)
    {
        var tokens = new List<SyntaxToken>();

        var lexer = new Lexer(text);

        SyntaxToken token;

        do
        {
            token = lexer.Lex();

            if (token.Kind is not SyntaxKind.WhitespaceToken
                and not SyntaxKind.BadToken)
            {
                tokens.Add(token);
            }
        }
        while (token.Kind != SyntaxKind.EndOfFileToken);

        _tokens = [.. tokens];

        Diagnostics.AddRange(lexer.Diagnostics);
    }

    public DiagnosticBag Diagnostics { get; } = [];

    private SyntaxToken Current => Peek(0);

    public SyntaxTree Parse()
    {
        var expression = ParseExpression();

        var endOfFileToken = MatchToken(SyntaxKind.EndOfFileToken);

        return new SyntaxTree(Diagnostics, expression, endOfFileToken);
    }

    private ExpressionSyntax ParsePrimaryExpression()
    {
        switch (Current.Kind)
        {
            case SyntaxKind.OpenParenthesisToken:
                {
                    var left = NextToken();

                    var expression = ParseExpression();

                    var right = MatchToken(SyntaxKind.CloseParenthesisToken);

                    return new ParenthesizedExpressionSyntax(left, expression, right);
                }

            case SyntaxKind.FalseKeyword or SyntaxKind.TrueKeyword:
                {
                    var keywordToken = NextToken();

                    var value = keywordToken.Kind == SyntaxKind.TrueKeyword;

                    return new LiteralExpressionSyntax(keywordToken, value);
                }

            case SyntaxKind.IdentifierToken:
                {
                    var identifierToken = NextToken();

                    return new NameExpressionSyntax(identifierToken);
                }

            default:
                {
                    var numberToken = MatchToken(SyntaxKind.NumberToken);

                    return new LiteralExpressionSyntax(numberToken);
                }
        }
    }

    private SyntaxToken Peek(int offset)
    {
        var index = _position + offset;

        return index >= _tokens.Length
            ? _tokens[^1]
            : _tokens[index];
    }

    private SyntaxToken NextToken()
    {
        var current = Current;

        _position++;

        return current;
    }

    private ExpressionSyntax ParseExpression(int parentPrecedence = 0)
    {
        return ParseAssignmentExpression(parentPrecedence);
    }

    private ExpressionSyntax ParseAssignmentExpression(int parentPrecedence = 0)
    {
        if (Peek(0).Kind == SyntaxKind.IdentifierToken
            && Peek(1).Kind == SyntaxKind.EqualsToken)
        {
            var identifierToken = NextToken();

            var operatorToken = NextToken();

            var right = ParseAssignmentExpression();

            return new AssignmentExpressionSyntax(identifierToken, operatorToken, right);
        }

        return ParseBinaryExpression(parentPrecedence);
    }

    private ExpressionSyntax ParseBinaryExpression(int parentPrecedence = 0)
    {
        ExpressionSyntax left;

        var unaryOperatorPrecedence = Current.Kind.GetUnaryOperatorPrecedence();

        if (unaryOperatorPrecedence != 0 && unaryOperatorPrecedence >= parentPrecedence)
        {
            var operatorToken = NextToken();

            var operand = ParseBinaryExpression(unaryOperatorPrecedence);

            left = new UnaryExpressionSyntax(operatorToken, operand);
        }
        else
        {
            left = ParsePrimaryExpression();
        }

        while (true)
        {
            var precedence = Current.Kind.GetBinaryOperatorPrecedence();

            if (precedence == 0 || precedence <= parentPrecedence)
            {
                break;
            }

            var operatorToken = NextToken();

            var right = ParseBinaryExpression(precedence);

            left = new BinaryExpressionSyntax(left, operatorToken, right);
        }

        return left;
    }

    private SyntaxToken MatchToken(SyntaxKind kind)
    {
        if (Current.Kind == kind)
        {
            return NextToken();
        }

        Diagnostics.ReportUnexpectedToken(Current.Span, Current.Kind, kind);

        return new SyntaxToken(kind, Current.Position);
    }
}
