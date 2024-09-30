using System;
using System.Collections.Generic;
using System.Linq;

namespace Marketer.ConsoleHelpers;

public static class TableDisplayHelper
{
    public static string BuildHeaderString(IList<string> headers)
    {
        var headerString = headers.Aggregate("| ", (current, header) => current + header.PadRight(header.Length) + " | ");
        return headerString.Remove(headerString.Length - 1);
    }

    public static string BuildSplitter(string headerString)
    {
        return new string('-', headerString.Length);
    }

    public static void DisplayHeadersAndSplitter(IList<string> headers)
    {
        var headerString = BuildHeaderString(headers);
        var splitter = BuildSplitter(headerString);

        Console.WriteLine(splitter);
        Console.WriteLine(headerString);
        Console.WriteLine(splitter);
    }

    public static void DisplayRow(IList<string> values, IList<string> headers)
    {
        var row = values.Select((value, index) => $"| {value.PadRight(headers[index].Length)} ").Aggregate((a, b) => a + b) + "|";
        Console.WriteLine(row);
    }
}