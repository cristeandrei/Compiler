namespace Compiler.CodeAnalysis;

using Enums;

internal sealed class ParenthesizedExpressionSyntax(
    SyntaxToken openParenthesisToken,
    ExpressionSyntax expression,
    SyntaxToken closeParenthesisToken)
    : ExpressionSyntax
{
    public override SyntaxKind Kind => SyntaxKind.ParenthesizedExpression;

    public SyntaxToken OpenParenthesisToken { get; } = openParenthesisToken;

    public ExpressionSyntax Expression { get; } = expression;

    public SyntaxToken CloseParenthesisToken { get; } = closeParenthesisToken;

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return OpenParenthesisToken;
        yield return Expression;
        yield return CloseParenthesisToken;
    }
}