using Compiler.Binding.Enums;

namespace Compiler.Binding;

internal class BoundAssignmentExpression(VariableSymbol variable, BoundExpression expression) : BoundExpression
{
    public VariableSymbol Variable { get; } = variable;

    public BoundExpression Expression { get; } = expression;

    public override BoundNodeKind Kind => BoundNodeKind.AssignmentExpression;

    public override Type Type => Variable.Type;
}
