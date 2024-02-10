using Compiler.Binding.Enums;

namespace Compiler.Binding;

internal sealed class BoundBinaryExpression(BoundExpression left, BoundBinaryOperator op, BoundExpression right) : BoundExpression
{
    public BoundExpression Left { get; } = left;

    public BoundBinaryOperator Op { get; } = op;

    public BoundExpression Right { get; } = right;

    public override BoundNodeKind Kind => BoundNodeKind.BinaryExpression;

    public override Type Type => Op.Type;
}