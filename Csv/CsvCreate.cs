#:project ../Shared
#:property PublishAot=false

using FileManagementOS.Shared;

string path = DataPaths.CsvFile;
Console.WriteLine("=== Program 1: CSV Create ===");

try
{
    List<Person> people = RecordGenerator.Generate(100, batch: 1);
    Directory.CreateDirectory(DataPaths.DataDir);

    // create a new file in the "path" variable.
    using (var writer = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write)))
    {
        foreach (Person person in people)
        {
            // write each record as a csv line 
            writer.WriteLine(person.ToCsvLine());
        }
    }

    Console.WriteLine($"Wrote {people.Count} records to {path}");
    Console.WriteLine($"Records now in file: {CsvFileInfo.CountRecords(path)}");
}
catch (Exception ex) when (ex is IOException or UnauthorizedAccessException)
{
    Console.Error.WriteLine($"CSV create failed: could not write {path}. {ex.Message}");
    return 1;
}
catch (InvalidDataException ex)
{
    Console.Error.WriteLine($"CSV create failed: bad record data. {ex.Message}");
    return 1;
}

return 0;
