using Compiler.Binding.Enums;

namespace Compiler.Binding;

internal sealed class BoundBinaryExpression(BoundExpression left, BoundBinaryOperatorKind operatorKind, BoundExpression right) : BoundExpression
{
    public BoundExpression Left { get; } = left;

    public BoundBinaryOperatorKind OperatorKind { get; } = operatorKind;

    public BoundExpression Right { get; } = right;

    public override BoundNodeKind Kind => BoundNodeKind.UnaryExpression;

    public override Type Type => Left.Type;
}