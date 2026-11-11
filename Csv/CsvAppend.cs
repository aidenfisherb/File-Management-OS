// reopens data/csv_demo.csv in append mode and adds 50 records after the existing ones.
#:project ../Shared
#:property PublishAot=false

using FileManagementOS.Shared;

string path = DataPaths.CsvFile;
Console.WriteLine("=== Program 2: CSV Append ===");

// FileMode.Append would quietly CREATE a missing file (leaving only 50 records), so check first.
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

    // FileMode.Append: start writing at the end; existing bytes are never touched.
    using (var writer = new StreamWriter(new FileStream(path, FileMode.Append, FileAccess.Write)))
    {
        if (needsNewline)
        {
            writer.WriteLine(); // make sure the first new record starts on its own line
        }

        foreach (Person person in people)
        {
            writer.WriteLine(person.ToCsvLine());
        }
    }

    Console.WriteLine($"Appended {people.Count} records to {path}");
    Console.WriteLine($"Records before: {before}  after: {CsvFileInfo.CountRecords(path)}");
}
catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
{
    // file access error: file deleted mid-run, locked, no permission...
    Console.Error.WriteLine($"CSV append failed: could not write {path}. {ex.Message}");
    return 1;
}
catch (InvalidDataException ex)
{
    // data error: a value would break the CSV format
    Console.Error.WriteLine($"CSV append failed: bad record data. {ex.Message}");
    return 1;
}

return 0;
