namespace Compiler.CodeAnalysis;

using Enums;

internal sealed class LiteralExpressionSyntax(SyntaxToken literalToken) : ExpressionSyntax
{
    public override SyntaxKind Kind => SyntaxKind.NumberExpression;

    public SyntaxToken LiteralToken { get; } = literalToken;

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return LiteralToken;
    }
}