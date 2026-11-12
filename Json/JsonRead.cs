#!/usr/bin/env dotnet
// Opens data/json_demo.json, parses the array into a list, prints a labeled table.
#:project ../Shared
#:property PublishAot=false
#:include Parsing/JsonParser.cs

using System.Text.Json;
using FileManagementOS.Shared;

string path = DataPaths.JsonFile;
Console.WriteLine("=== Program 6: JSON Read ===");

if (!File.Exists(path))
{
    Console.Error.WriteLine($"JSON read failed: {path} not found. Run JSON Create first.");
    return 1;
}

try
{
    List<Person> people = new JsonParser(path).Parse();

    // same column labels and widths as the CSV table
    Console.WriteLine(
        $"{"First",-10} {"Last",-10} {"DOB",-12} {"Phone",-14} {"Street",-22} {"City",-12} {"St",-2} {"ZIP",-5}");
    foreach (Person p in people)
    {
        Console.WriteLine(
            $"{p.FirstName,-10} {p.LastName,-10} {p.DateOfBirth,-12} {p.Phone,-14} {p.Street,-22} {p.City,-12} {p.State,-2} {p.Zip,-5}");
    }

    // count parsed objects, not lines in the file
    Console.WriteLine($"Records read: {people.Count}");
}
catch (JsonException ex)
{
    // data parsing error: the file opened fine but its contents are not a valid array of records
    Console.Error.WriteLine($"JSON read failed: could not parse {path}. {ex.Message}");
    return 1;
}
catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
{
    // file access error: locked file, no permission...
    Console.Error.WriteLine($"JSON read failed: could not read {path}. {ex.Message}");
    return 1;
}

return 0;
