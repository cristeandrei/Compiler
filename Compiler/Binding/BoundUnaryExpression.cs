using Compiler.Binding.Enums;

namespace Compiler.Binding;

internal class BoundUnaryExpression(BoundUnaryOperator op, BoundExpression operand)
    : BoundExpression
{
    public override BoundNodeKind Kind => BoundNodeKind.UnaryExpression;

    public override Type Type => Op.Type;

    public BoundUnaryOperator Op { get; } = op;

    public BoundExpression Operand { get; } = operand;
}