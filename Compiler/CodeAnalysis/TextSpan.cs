namespace Compiler.CodeAnalysis;

internal record struct TextSpan(int Start, int Length)
{
    public readonly int End => Start + Length;
}

internal class Diagnostic(TextSpan span, string message)
{
    public TextSpan Span { get; } = span;

    public string Message { get; } = message;

    public override string ToString()
    {
        return $"{nameof(Message)}: {Message}";
    }
}
