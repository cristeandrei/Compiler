namespace Compiler.CodeAnalysis;

internal class EvaluationResult(IEnumerable<string> diagnostics, object? value = null)
{
    public IEnumerable<string> Diagnostics { get; } = diagnostics;

    public object? Value { get; } = value;
}