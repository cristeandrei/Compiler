using Compiler.CodeAnalysis.Enums;

namespace Compiler.CodeAnalysis;

internal class NameExpressionSyntax(SyntaxToken identifierToken) : ExpressionSyntax
{
    public SyntaxToken IdentifierToken { get; } = identifierToken;

    public override SyntaxKind Kind => SyntaxKind.NameExpression;

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        yield return IdentifierToken;
    }
}
