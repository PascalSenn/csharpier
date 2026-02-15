using CSharpier.Core.DocTypes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpier.Core.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class ParenthesizedExpression
{
    public static Doc Print(ParenthesizedExpressionSyntax node, PrintingContext context)
    {
        return Doc.Group(
            Token.Print(node.OpenParenToken, context),
            Doc.Indent(Doc.SoftLine, Node.Print(node.Expression, context)),
            Token.Print(node.CloseParenToken, context)
        );
    }
}
