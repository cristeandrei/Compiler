// <copyright file="SyntaxKind.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Compiler.CodeAnalysis.Enums;

internal enum SyntaxKind
{
    // tokens
    BadToken,
    EndOfFileToken,
    WhitespaceToken,
    NumberToken,
    PlusToken,
    MinusToken,
    StarToken,
    SlashToken,
    OpenParenthesisToken,
    CloseParenthesisToken,

    // expressions
    BinaryExpression,
    ParenthesizedExpression,
    LiteralExpression,
    UnaryExpression,

    TrueKeyword,
    FalseKeyword,
    IdentifierToken,
}