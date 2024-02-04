using Compiler.Binding.Enums;
using Compiler.CodeAnalysis;
using Compiler.CodeAnalysis.Enums;

namespace Compiler.Binding;

internal sealed class Binder
{
    public IList<string> Diagnostics { get; } = [];

    public BoundExpression BindExpression(ExpressionSyntax syntax)
    {
        return syntax.Kind switch
        {
            SyntaxKind.LiteralExpression => BindLiteralExpression((LiteralExpressionSyntax)syntax),
            SyntaxKind.UnaryExpression => BindUnaryExpression((UnaryExpressionSyntax)syntax),
            SyntaxKind.BinaryExpression => BindBinaryExpression((BinaryExpressionSyntax)syntax),
            _ => throw new ArgumentException($"Unexpected syntax {syntax.Kind}."),
        };
    }

    private static BoundExpression BindLiteralExpression(LiteralExpressionSyntax syntax)
    {
        var value = syntax.Value;

        return new BoundLiteralExpression(value);
    }

    private BoundExpression BindUnaryExpression(UnaryExpressionSyntax syntax)
    {
        var boundOperand = BindExpression(syntax.Operand);
        var boundOperatorKind = BoundUnaryOperator.Bind(syntax.OperatorToken.Kind, boundOperand.Type);

        if (boundOperatorKind == null)
        {
            Diagnostics.Add($"Binary operator `{syntax.OperatorToken.Text}` is not defined for type {boundOperand.Type}.");

            return boundOperand;
        }

        return new BoundUnaryExpression(boundOperatorKind, boundOperand);
    }

    private BoundExpression BindBinaryExpression(BinaryExpressionSyntax syntax)
    {
        var boundLeft = BindExpression(syntax.Left);
        var boundRight = BindExpression(syntax.Right);
        var boundOperatorKind = BoundBinaryOperator.Bind(syntax.OperatorToken.Kind, boundLeft.Type, boundRight.Type);

        if (boundOperatorKind == null)
        {
            Diagnostics.Add($"Binary operator `{syntax.OperatorToken.Text}` is not defined for type {boundLeft.Type} and {boundRight.Type}.");

            return boundLeft;
        }

        return new BoundBinaryExpression(boundLeft, boundOperatorKind, boundRight);
    }
}