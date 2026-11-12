#!/usr/bin/env dotnet
// Reads data/json_demo.json, parses the array, adds 50 records to the end, rewrites the file as valid JSON.
#:project ../Shared
#:property PublishAot=false
#:include ../Parsing/JsonParser.cs

using System.Text.Json;
using FileManagementOS.Shared;

string path = DataPaths.JsonFile;
Console.WriteLine("=== Program 5: JSON Add ===");

if (!File.Exists(path))
{
    Console.Error.WriteLine($"JSON add failed: {path} not found. Run JSON Create first.");
    return 1;
}

try
{
    // Step 1: parse the existing array. If this throws, nothing below runs, so the file is left untouched.
    List<Person> people = new JsonParser(path).Parse();
    List<Person> original = [.. people]; // copy of the original records, used to check them afterwards
    int before = people.Count;

    // Step 2: add the new records to the collection in memory, after the originals.
    people.AddRange(RecordGenerator.Generate(50, batch: 2));

    // Step 3: write the whole array to a temp file, then swap it in for the real file.
    // We can't append like CSV: bytes after the closing ']' would make the JSON invalid.
    // The temp file means a crash mid-write can't leave a half-written json_demo.json.
    string tempPath = path + ".tmp";
    using (var stream = new FileStream(tempPath, FileMode.Create, FileAccess.Write))
    {
        JsonSerializer.Serialize(stream, people, PersonJson.Options);
    }
    File.Move(tempPath, path, overwrite: true);

    // Step 4: read the file back to prove it is valid JSON and the originals survived in order.
    List<Person> check = new JsonParser(path).Parse();
    bool preserved = check.Take(before).SequenceEqual(original);

    Console.WriteLine($"Added {check.Count - before} records to {path}");
    Console.WriteLine($"Records before: {before}  after: {check.Count}");
    Console.WriteLine($"Original {before} records unchanged and in order: {(preserved ? "yes" : "NO")}");
}
catch (JsonException ex)
{
    // data parsing error: the file opened fine but its contents are not a valid array of records
    Console.Error.WriteLine($"JSON add failed: could not parse {path}; the file was not changed. {ex.Message}");
    return 1;
}
catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
{
    // file access error: file deleted mid-run, locked, no permission, disk full...
    Console.Error.WriteLine($"JSON add failed: could not read or write {path}. {ex.Message}");
    return 1;
}

return 0;
