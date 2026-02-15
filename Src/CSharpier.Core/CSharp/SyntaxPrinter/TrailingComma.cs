using CSharpier.Core.DocTypes;
using CSharpier.Core.Utilities;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace CSharpier.Core.CSharp.SyntaxPrinter;

internal static class TrailingComma
{
    public static Doc Print(
        SyntaxToken closingToken,
        PrintingContext context,
        bool skipIfBreak = false
    )
    {
        return Doc.Null;
    }
}
