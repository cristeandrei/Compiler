using Compiler.CodeAnalysis.Enums;

namespace Compiler.CodeAnalysis;

internal class AssignmentExpressionSyntax(SyntaxToken identifierToken, SyntaxToken equalsToken, ExpressionSyntax expression) : ExpressionSyntax
{
    public SyntaxToken IdentifierToken { get; } = identifierToken;

    public SyntaxToken EqualsToken { get; } = equalsToken;

    public ExpressionSyntax Expression { get; } = expression;

    public override SyntaxKind Kind => SyntaxKind.AssignmentExpression;

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return IdentifierToken;
        yield return EqualsToken;
        yield return EqualsToken;
    }
}
