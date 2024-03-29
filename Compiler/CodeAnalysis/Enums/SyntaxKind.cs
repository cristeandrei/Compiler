﻿// <copyright file="SyntaxKind.cs" company="PlaceholderCompany">
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
    NameExpression,
    LiteralExpression,
    UnaryExpression,

    TrueKeyword,
    FalseKeyword,
    IdentifierToken,
    BangToken,

    AmpersandAmpersandToken,
    PipePipeToken,
    EqualsEqualsToken,
    BangEqualsToken,
    AssignmentExpression,
    EqualsToken,
}
