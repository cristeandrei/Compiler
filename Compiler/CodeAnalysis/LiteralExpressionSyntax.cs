namespace Compiler.CodeAnalysis;

using Enums;

internal sealed class LiteralExpressionSyntax(SyntaxToken literalToken, object value) : ExpressionSyntax
{
    public LiteralExpressionSyntax(SyntaxToken literalToken)
        : this(literalToken, literalToken.Value)
    {
    }

    public override SyntaxKind Kind => SyntaxKind.LiteralExpression;

    public SyntaxToken LiteralToken { get; } = literalToken;

    public object Value { get; } = value ?? string.Empty;

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return LiteralToken;
    }
}