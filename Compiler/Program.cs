using Compiler.Binding;
using Compiler.CodeAnalysis;

var showTree = false;

while (true)
{
    Console.Write("> ");

    var line = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(line))
    {
        return;
    }

    switch (line)
    {
        case "#showTree":
            showTree = !showTree;
            Console.WriteLine(showTree ? "Showing parse trees." : "Not showing parse trees");
            continue;
        case "#cls":
            Console.Clear();
            continue;
    }

    var syntaxTree = SyntaxTree.Parse(line);

    if (showTree)
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;

        PrettyPrint(syntaxTree.Root);

        Console.ResetColor();
    }

    var binder = new Binder();

    var boundExpression = binder.BindExpression(syntaxTree.Root);

    var diagnostics = syntaxTree
        .Diagnostics
        .Concat(binder.Diagnostics);

    if (!diagnostics.Any())
    {
        var e = new Evaluator(boundExpression);

        var result = e.Evaluate();

        Console.WriteLine(result);
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.DarkRed;

        foreach (var diagnostic in diagnostics)
        {
            Console.WriteLine(diagnostic);
        }

        Console.ResetColor();
    }
}

static void PrettyPrint(SyntaxNode node, string indent = "", bool isLast = true)
{
    var marker = isLast ? "└──" : "├──";

    Console.Write(indent);
    Console.Write(marker);
    Console.Write(node.Kind);

    if (node is SyntaxToken { Value: not null } t)
    {
        Console.Write(" ");
        Console.Write(t.Value);
    }

    Console.WriteLine();

    indent += isLast ? "   " : "│   ";

    var lastChild = node.GetChildren().LastOrDefault();

    foreach (var child in node.GetChildren())
    {
        PrettyPrint(child, indent, child == lastChild);
    }
}