namespace CSharpier.SyntaxPrinter.SyntaxNodePrinters;

internal static class IfStatement
{
    public static Doc Print(IfStatementSyntax node, FormattingContext context)
    {
        var docs = new List<Doc>();
        if (node.Parent is not ElseClauseSyntax)
        {
            docs.Add(ExtraNewLines.Print(node));
        }

        docs.Add(
            Token.Print(node.IfKeyword, context),
            " ",
            Doc.Group(
                Token.Print(node.OpenParenToken, context),
                Doc.Indent(Node.Print(node.Condition, context))
            ),
            Token.Print(node.CloseParenToken, context),
            OptionalBraces.Print(node.Statement, context)
        );

        if (node.Else != null)
        {
            docs.Add(Doc.HardLine, Node.Print(node.Else, context));
        }

        return Doc.Concat(docs);
    }
}
