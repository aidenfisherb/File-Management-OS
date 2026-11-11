// Creates data/csv_demo.csv and writes 100 records, one per line, no header row.
#:project ../Shared
#:property PublishAot=false

using FileManagementOS.Shared;

string path = DataPaths.CsvFile;
Console.WriteLine("=== Program 1: CSV Create ===");

try
{
    List<Person> people = RecordGenerator.Generate(100, batch: 1);
    Directory.CreateDirectory(DataPaths.DataDir);

    // FileMode.Create: make the file, or wipe an existing one to 0 bytes.
    // "using" closes the file at the end of the block, even if an error is thrown.
    using (var writer = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write)))
    {
        foreach (Person person in people)
        {
            writer.WriteLine(person.ToCsvLine());
        }
    }

    Console.WriteLine($"Wrote {people.Count} records to {path}");
    Console.WriteLine($"Records now in file: {CsvFileInfo.CountRecords(path)}");
}
catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
{
    // file access error: locked file, no permission, disk full...
    Console.Error.WriteLine($"CSV create failed: could not write {path}. {ex.Message}");
    return 1;
}
catch (InvalidDataException ex)
{
    // data error: a value would break the CSV format
    Console.Error.WriteLine($"CSV create failed: bad record data. {ex.Message}");
    return 1;
}

return 0;
