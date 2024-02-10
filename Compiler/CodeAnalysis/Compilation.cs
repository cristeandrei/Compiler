using Compiler.Binding;

namespace Compiler.CodeAnalysis;

internal class Compilation(SyntaxTree syntaxTree)
{
    public SyntaxTree SyntaxTree { get; } = syntaxTree;

    public EvaluationResult Evaluate()
    {
        var binder = new Binder();

        var boundExpression = binder.BindExpression(SyntaxTree.Root);

        var diagnostics = SyntaxTree
            .Diagnostics
            .Concat(binder.Diagnostics)
            .ToList();

        if (diagnostics.Count != 0)
        {
            return new EvaluationResult(diagnostics);
        }

        var evaluator = new Evaluator(boundExpression);

        var value = evaluator.Evaluate();

        return new EvaluationResult(diagnostics, value);
    }
}
