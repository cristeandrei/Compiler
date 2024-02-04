using Compiler.Binding;
using Compiler.Binding.Enums;

namespace Compiler.CodeAnalysis;

internal class Evaluator(BoundExpression root)
{
    public int Evaluate()
    {
        return EvaluateExpression(root);
    }

    private static int EvaluateExpression(BoundExpression node)
    {
        switch (node)
        {
            case BoundLiteralExpression n:
                return (int)n.Value;
            case BoundUnaryExpression u:
                var operand = EvaluateExpression(u.Operand);

                return u.OperatorKind switch
                {
                    BoundUnaryOperatorKind.Identity => operand,
                    BoundUnaryOperatorKind.Negation => -operand,
                    _ => throw new ArgumentOutOfRangeException($"Unexpected unary operator {u.OperatorKind}"),
                };
            case BoundBinaryExpression b:
                {
                    var left = EvaluateExpression(b.Left);
                    var right = EvaluateExpression(b.Right);

                    return b.OperatorKind switch
                    {
                        BoundBinaryOperatorKind.Addition => left + right,
                        BoundBinaryOperatorKind.Subtraction => left - right,
                        BoundBinaryOperatorKind.Multiplication => left * right,
                        BoundBinaryOperatorKind.Division => left / right,
                        _ => throw new ArgumentException($"Unexpected binary operator {b.OperatorKind}"),
                    };
                }

            default:
                throw new AggregateException($"Unexpected node {node.Kind}");
        }
    }
}