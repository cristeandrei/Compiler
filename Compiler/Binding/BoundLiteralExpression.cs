using Compiler.Binding.Enums;

namespace Compiler.Binding;

internal sealed class BoundLiteralExpression(object value) : BoundExpression
{
    public object Value { get; } = value;

    public override BoundNodeKind Kind => BoundNodeKind.LiteralExpression;

    public override Type Type => Value.GetType();
}