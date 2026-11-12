#:project ../Shared
#:property PublishAot=false

using FileManagementOS.Shared;

string path = DataPaths.CsvFile;
Console.WriteLine("=== Program 2: CSV Append ===");

// check that there's a file to append to
if (!File.Exists(path))
{
    Console.Error.WriteLine($"CSV append failed: {path} not found. Run CSV Create first.");
    return 1;
}

try
{
    List<Person> people = RecordGenerator.Generate(50, batch: 2);
    int before = CsvFileInfo.CountRecords(path);
    bool needsNewline = !CsvFileInfo.EndsWithNewline(path);

    // reopen the file in append mode
    using (var writer = new StreamWriter(new FileStream(path, FileMode.Append, FileAccess.Write)))
    {
        if (needsNewline)
        {
            // make sure the first new record starts on its own line
            writer.WriteLine();
        }

        foreach (Person person in people)
        {
            //write the line
            writer.WriteLine(person.ToCsvLine());
        }
    }

    Console.WriteLine($"Appended {people.Count} records to {path}");
    Console.WriteLine($"Records before: {before}  after: {CsvFileInfo.CountRecords(path)}");
}
catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
{
    Console.Error.WriteLine($"CSV append failed: could not write {path}. {ex.Message}");
    return 1;
}
catch (InvalidDataException ex)
{
    Console.Error.WriteLine($"CSV append failed: bad record data. {ex.Message}");
    return 1;
}

return 0;
