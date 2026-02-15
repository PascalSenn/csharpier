using CSharpier.Core.DocTypes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpier.Core.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class ArrowExpressionClause
{
    public static Doc Print(ArrowExpressionClauseSyntax node, PrintingContext context)
    {
        return Doc.Group(
            Doc.Indent(
                Doc.Line,
                Token.PrintWithSuffix(node.ArrowToken, " ", context),
                Node.Print(node.Expression, context)
            )
        );
    }
}
