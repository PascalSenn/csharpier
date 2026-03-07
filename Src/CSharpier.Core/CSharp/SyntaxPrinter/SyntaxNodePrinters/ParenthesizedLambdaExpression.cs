using System.Diagnostics;
using CSharpier.Core.DocTypes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CSharpier.Core.CSharp.SyntaxPrinter.SyntaxNodePrinters;

internal static class ParenthesizedLambdaExpression
{
    public static Doc Print(ParenthesizedLambdaExpressionSyntax node, PrintingContext context)
    {
        return Doc.Concat(PrintHead(node, context), PrintBody(node, context));
    }

    public static Doc PrintHead(ParenthesizedLambdaExpressionSyntax node, PrintingContext context)
    {
        return Doc.Concat(
            AttributeLists.Print(node, node.AttributeLists, context),
            Modifiers.PrintSorted(node.Modifiers, context),
            node.ReturnType != null
                ? Doc.Concat(Node.Print(node.ReturnType, context), " ")
                : Doc.Null,
            ParameterList.Print(node.ParameterList, context),
            " ",
            Token.Print(node.ArrowToken, context)
        );
    }

    public static Doc PrintBody(ParenthesizedLambdaExpressionSyntax node, PrintingContext context)
    {
        if (node.Body is BlockSyntax block)
        {
            if (
                block.Statements.Count == 1
                && block.Statements[0]
                    is ExpressionStatementSyntax
                        or ReturnStatementSyntax
                        or ThrowStatementSyntax
                        or LocalDeclarationStatementSyntax
                && !Token.HasComments(block.OpenBraceToken)
                && !Token.HasComments(block.CloseBraceToken)
                && !block.Statements[0].GetLeadingTrivia().Any(o => o.IsComment())
                && !block.Statements[0].GetTrailingTrivia().Any(o => o.IsComment())
            )
            {
                var statement = Node.Print(block.Statements[0], context);

                var inlineOption = Doc.Concat(
                    " ",
                    Token.Print(block.OpenBraceToken, context),
                    " ",
                    statement,
                    " ",
                    Token.Print(block.CloseBraceToken, context)
                );

                var brokenOption = Doc.Concat(
                    Doc.HardLine,
                    Block.Print(block, context)
                );

                return Doc.ConditionalGroup(inlineOption, brokenOption);
            }

            return Doc.Concat(
                block.Statements.Count > 0 ? Doc.HardLine : " ",
                Block.Print(block, context)
            );
        }

        var body = Node.Print(node.Body, context);

        if (
            node.ParameterList.Parameters.Count == 0
            && node.Parent?.Parent is ArgumentListSyntax { Arguments.Count: 1 }
        )
        {
            return Doc.IfBreak(Doc.Indent(Doc.Line, body), Doc.Group(Doc.Indent(Doc.Line, body)));
        }

        return Doc.Group(Doc.Indent(Doc.Line, body));
    }
}
