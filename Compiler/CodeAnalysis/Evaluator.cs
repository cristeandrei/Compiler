using Compiler.Binding;
using Compiler.Binding.Enums;

namespace Compiler.CodeAnalysis;

internal class Evaluator(BoundExpression root)
{
    public object Evaluate()
    {
        return EvaluateExpression(root);
    }

    private static object EvaluateExpression(BoundExpression node)
    {
        switch (node)
        {
            case BoundLiteralExpression n:
                return n.Value;
            case BoundUnaryExpression u:
                var operand = EvaluateExpression(u.Operand);

                return u.OperatorKind switch
                {
                    BoundUnaryOperatorKind.Identity => (int)operand,
                    BoundUnaryOperatorKind.Negation => -(int)operand,
                    BoundUnaryOperatorKind.LogicalNegation => !(bool)operand,
                    _ => throw new ArgumentOutOfRangeException($"Unexpected unary operator {u.OperatorKind}"),
                };
            case BoundBinaryExpression b:
                {
                    var left = EvaluateExpression(b.Left);
                    var right = EvaluateExpression(b.Right);

                    return b.OperatorKind switch
                    {
                        BoundBinaryOperatorKind.Addition => (int)left + (int)right,
                        BoundBinaryOperatorKind.Subtraction => (int)left - (int)right,
                        BoundBinaryOperatorKind.Multiplication => (int)left * (int)right,
                        BoundBinaryOperatorKind.Division => (int)left / (int)right,
                        BoundBinaryOperatorKind.LogicalAnd => (bool)left && (bool)right,
                        BoundBinaryOperatorKind.LogicalOr => (bool)left || (bool)right,
                        _ => throw new ArgumentException($"Unexpected binary operator {b.OperatorKind}"),
                    };
                }

            default:
                throw new AggregateException($"Unexpected node {node.Kind}");
        }
    }
}