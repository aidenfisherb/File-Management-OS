#!/usr/bin/env dotnet
// Opens data/csv_demo.csv, parses all records with TextFieldParser, prints a labeled table.
#:project ../Shared
#:property PublishAot=false
#:include CsvParser.cs


using FileManagementOS.Shared;
using Microsoft.VisualBasic.FileIO;

string path = DataPaths.CsvFile;
Console.WriteLine("=== Program 3: CSV Read ===");

if (!File.Exists(path))
{
    Console.Error.WriteLine($"CSV read failed: {path} not found. Run CSV Create first.");
    return 1;
}

try
{
    new CsvParser(path).Run();
}
catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
{
    Console.Error.WriteLine($"CSV read failed: could not read {path}. {ex.Message}");
    return 1;
}
catch (Exception ex) when (ex is MalformedLineException or InvalidDataException)
{
    Console.Error.WriteLine($"CSV read failed: bad CSV data in {path}. {ex.Message}");
    return 1;
}

return 0;

public partial class CsvParser
{
    public void Run()
    {
        var people = Parse();
        Console.WriteLine(
            $"{"First",-10} {"Last",-10} {"DOB",-12} {"Phone",-14} {"Street",-22} {"City",-12} {"St",-2} {"ZIP",-5}");
        foreach (var p in people)
            Console.WriteLine(
                $"{p.FirstName,-10} {p.LastName,-10} {p.DateOfBirth,-12} {p.Phone,-14} {p.Street,-22} {p.City,-12} {p.State,-2} {p.Zip,-5}");
        Console.WriteLine($"Records read: {people.Count}");
    }
}
