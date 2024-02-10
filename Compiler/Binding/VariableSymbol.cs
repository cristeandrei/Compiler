namespace Compiler.Binding;

internal class VariableSymbol(string name, Type type)
{
    public string Name { get; } = name;

    public Type Type { get; } = type;
}