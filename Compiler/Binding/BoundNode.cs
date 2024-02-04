using Compiler.Binding.Enums;
using Compiler.CodeAnalysis;
using Compiler.CodeAnalysis.Enums;

namespace Compiler.Binding;

internal abstract class BoundNode
{
    public abstract BoundNodeKind Kind { get; }
}
