namespace Compiler.CodeAnalysis;

internal class EvaluationResult(IEnumerable<Diagnostic> diagnostics, object? value = null)
{
    public IEnumerable<Diagnostic> Diagnostics { get; } = diagnostics;

    public object? Value { get; } = value;
}
