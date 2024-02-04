using Compiler.CodeAnalysis.Enums;

namespace Compiler.CodeAnalysis;

internal static class SyntaxFacts
{
    public static int GetBinaryOperatorPrecedence(this SyntaxKind kind)
    {
        return kind switch
        {
            SyntaxKind.StarToken or SyntaxKind.SlashToken => 2,
            SyntaxKind.PlusToken or SyntaxKind.MinusToken => 1,
            _ => 0
        };
    }
}