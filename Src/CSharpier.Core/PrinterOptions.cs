using System.Globalization;
using CSharpier.Core.DocTypes;

namespace CSharpier.Core;

internal class PrinterOptions(Formatter formatter)
{
    public bool IncludeAST { get; init; }
    public bool IncludeDocTree { get; init; }
    public bool UseTabs { get; set; }

    private int indentSize = formatter == Formatter.XML ? 2 : 4;
    public int IndentSize
    {
        get => this.indentSize;
        set
        {
            if (value <= 0)
            {
                throw new ArgumentException("An indent size of 0 is not valid");
            }
            this.indentSize = value;
        }
    }

    public int Width { get; set; } = 120;
    public EndOfLine EndOfLine { get; set; } = EndOfLine.Auto;
    public bool TrimInitialLines { get; init; } = true;
    public bool IncludeGenerated { get; set; }
    public Formatter Formatter { get; set; } = formatter;

    public double SmallLineFactor { get; set; } = 0.33;
    public double MediumLineFactor { get; set; } = 0.66;
    public double LongLineFactor { get; set; } = 0.8;

    public const int WidthUsedByTests = 120;

    public int GetEffectiveWidth(LineLengthThreshold threshold)
    {
        return threshold switch
        {
            LineLengthThreshold.Small => (int)(Width * SmallLineFactor),
            LineLengthThreshold.Medium => (int)(Width * MediumLineFactor),
            LineLengthThreshold.Long => (int)(Width * LongLineFactor),
            _ => Width,
        };
    }

    internal static string GetLineEnding(string code, PrinterOptions printerOptions)
    {
        if (printerOptions.EndOfLine != EndOfLine.Auto)
        {
            return printerOptions.EndOfLine == EndOfLine.CRLF ? "\r\n" : "\n";
        }

        var lineIndex = code.IndexOf('\n');
        if (lineIndex <= 0)
        {
            return "\n";
        }
        if (code[lineIndex - 1] == '\r')
        {
            return "\r\n";
        }

        return "\n";
    }

    public static Formatter GetFormatter(string filePath)
    {
        var possibleExtension = Path.GetExtension(filePath);
        if (possibleExtension == string.Empty)
        {
            return Formatter.Unknown;
        }

        var extension = possibleExtension[1..].ToLower(CultureInfo.InvariantCulture);

        var formatter = extension switch
        {
            "cs" => Formatter.CSharp,
            "csx" => Formatter.CSharpScript,
            "config" or "csproj" or "props" or "slnx" or "targets" or "xaml" or "xml" =>
                Formatter.XML,
            _ => Formatter.Unknown,
        };
        return formatter;
    }
}

internal enum Formatter
{
    Unknown,
    CSharp,
    CSharpScript,
    XML,
}
