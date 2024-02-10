using System.Collections;
using Compiler.CodeAnalysis.Enums;

namespace Compiler.CodeAnalysis;

internal class DiagnosticBag : IEnumerable<Diagnostic>
{
    private readonly List<Diagnostic> _diagnostics = [];

    public void Report(TextSpan span, string message)
    {
        var diagnostic = new Diagnostic(span, message);

        _diagnostics.Add(diagnostic);
    }

    public IEnumerator<Diagnostic> GetEnumerator()
    {
        return _diagnostics.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void ReportInvalidNumber(TextSpan span, string text, Type type)
    {
        var message = $"The number {text} isn't valid {type}.";

        Report(span, message);
    }

    public void ReportBadCharacter(TextSpan span, char character)
    {
        var message = $"Bad character input: '{character}'.";

        Report(span, message);
    }

    public void AddRange(DiagnosticBag diagnosticBag)
    {
        _diagnostics.AddRange(diagnosticBag);
    }

    public void ReportUnexpectedToken(TextSpan span, SyntaxKind currentKind, SyntaxKind expectedKind)
    {
        var message = $"Unexpected token <{currentKind}>, expected <{expectedKind}>.";

        Report(span, message);
    }

    public void ReportUndefinedUnaryOperator(TextSpan span, string operatorText, Type operandType)
    {
        var message = $"Binary operator `{operatorText}` is not defined for type {operandType}.";

        Report(span, message);
    }

    public void ReportUndefinedBinaryOperator(TextSpan span, string operatorText, Type leftType, Type rightType)
    {
        var message =
            $"Binary operator `{operatorText}` is not defined for type {leftType} and {rightType}.";

        Report(span, message);
    }

    public void ReportUndefinedName(TextSpan span, string name)
    {
        var message =
            $"Variable '{name}' does not exist.";

        Report(span, message);
    }
}
