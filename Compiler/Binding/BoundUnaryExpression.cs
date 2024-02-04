using Compiler.Binding.Enums;

namespace Compiler.Binding;

internal class BoundUnaryExpression(BoundUnaryOperatorKind operatorKind, BoundExpression operand)
    : BoundExpression
{
    public override BoundNodeKind Kind => BoundNodeKind.UnaryExpression;

    public override Type Type => Operand.Type;

    public BoundUnaryOperatorKind OperatorKind { get; } = operatorKind;

    public BoundExpression Operand { get; } = operand;
}