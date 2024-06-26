namespace CSharpier.SyntaxPrinter.SyntaxNodePrinters;

internal static class TypeParameterList
{
    public static Doc Print(TypeParameterListSyntax node, FormattingContext context)
    {
        if (node.Parameters.Count == 0)
        {
            return Doc.Null;
        }

        var shouldBreakMore =
            node.Parameters.Count > 1 || node.Parameters.First().AttributeLists.Any();

        return Doc.Group(
            Token.Print(node.LessThanToken, context),
            Doc.Indent(
                shouldBreakMore ? Doc.SoftLine : Doc.Null,
                SeparatedSyntaxList.Print(node.Parameters, TypeParameter.Print, Doc.Line, context)
            ),
            Token.Print(node.GreaterThanToken, context)
        );
    }
}
