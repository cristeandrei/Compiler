namespace Compiler.CodeAnalysis;

using Enums;

internal class Evaluator(ExpressionSyntax root)
{
    public int Evaluate()
    {
        return EvaluateExpression(root);
    }

    private static int EvaluateExpression(ExpressionSyntax node)
    {
        switch (node)
        {
            case LiteralExpressionSyntax n:
                return (int)n.LiteralToken.Value;
            case UnaryExpressionSyntax u:
                var operand = EvaluateExpression(u.Operand);

                return u.OperatorToken.Kind switch
                {
                    SyntaxKind.PlusToken => operand,
                    SyntaxKind.MinusToken => -operand,
                    _ => throw new ArgumentOutOfRangeException($"Unexpected unary operator {u.OperatorToken.Kind}"),
                };
            case BinaryExpressionSyntax b:
                {
                    var left = EvaluateExpression(b.Left);
                    var right = EvaluateExpression(b.Right);

                    return b.OperatorToken.Kind switch
                    {
                        SyntaxKind.PlusToken => left + right,
                        SyntaxKind.MinusToken => left - right,
                        SyntaxKind.StarToken => left * right,
                        SyntaxKind.SlashToken => left / right,
                        _ => throw new ArgumentException($"Unexpected binary operator {b.OperatorToken.Kind}"),
                    };
                }

            case ParenthesizedExpressionSyntax p:
                return EvaluateExpression(p.Expression);
            default:
                throw new AggregateException($"Unexpected node {node.Kind}");
        }
    }
}