namespace Compiler.CodeAnalysis;

using Enums;

internal class SyntaxToken(SyntaxKind kind, int position, string text = "", object? value = default)
    : SyntaxNode
{
    public override SyntaxKind Kind { get; } = kind;

    public int Position { get; } = position;

    public string Text { get; } = text;

    public object Value { get; } = value ?? string.Empty;

    public override IEnumerable<SyntaxNode> GetChildren()
    {
        return Enumerable.Empty<SyntaxNode>();
    }
}