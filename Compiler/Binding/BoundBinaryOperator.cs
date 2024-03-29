using Compiler.Binding.Enums;
using Compiler.CodeAnalysis.Enums;

namespace Compiler.Binding;

internal sealed class BoundBinaryOperator
{
    private static readonly BoundBinaryOperator[] _operators = [
        new BoundBinaryOperator(SyntaxKind.PlusToken, BoundBinaryOperatorKind.Addition, typeof(int)),
        new BoundBinaryOperator(SyntaxKind.MinusToken, BoundBinaryOperatorKind.Subtraction, typeof(int)),
        new BoundBinaryOperator(SyntaxKind.StarToken, BoundBinaryOperatorKind.Multiplication, typeof(int)),
        new BoundBinaryOperator(SyntaxKind.SlashToken, BoundBinaryOperatorKind.Division, typeof(int)),

        new BoundBinaryOperator(SyntaxKind.EqualsEqualsToken, BoundBinaryOperatorKind.Equals, typeof(int), typeof(bool)),
        new BoundBinaryOperator(SyntaxKind.BangEqualsToken, BoundBinaryOperatorKind.NotEquals, typeof(int), typeof(bool)),

        new BoundBinaryOperator(SyntaxKind.EqualsEqualsToken, BoundBinaryOperatorKind.Equals, typeof(bool)),
        new BoundBinaryOperator(SyntaxKind.BangEqualsToken, BoundBinaryOperatorKind.NotEquals, typeof(BoundBinaryOperatorKind)),

        new BoundBinaryOperator(SyntaxKind.AmpersandAmpersandToken, BoundBinaryOperatorKind.LogicalAnd, typeof(bool)),
        new BoundBinaryOperator(SyntaxKind.PipePipeToken, BoundBinaryOperatorKind.LogicalOr, typeof(bool)),
    ];

    private BoundBinaryOperator(SyntaxKind syntaxKind, BoundBinaryOperatorKind kind, Type operandsType, Type type)
        : this(syntaxKind, kind, operandsType, operandsType, type)
    {
    }

    private BoundBinaryOperator(SyntaxKind syntaxKind, BoundBinaryOperatorKind kind, Type type)
    : this(syntaxKind, kind, type, type, type)
    {
    }

    private BoundBinaryOperator(SyntaxKind syntaxKind, BoundBinaryOperatorKind kind, Type leftType, Type rightType, Type type)
    {
        SyntaxKind = syntaxKind;
        Kind = kind;
        LeftType = leftType;
        RightType = rightType;
        Type = type;
    }

    public SyntaxKind SyntaxKind { get; }

    public BoundBinaryOperatorKind Kind { get; }

    public Type LeftType { get; }

    public Type RightType { get; }

    public Type Type { get; }

    public static BoundBinaryOperator? Bind(SyntaxKind syntaxKind, Type leftType, Type rightType)
    {
        foreach (var op in _operators)
        {
            if (op.SyntaxKind == syntaxKind
                && op.LeftType == leftType
                && op.RightType == rightType)
            {
                return op;
            }
        }

        return null;
    }
}