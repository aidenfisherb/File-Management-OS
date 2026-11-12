using System.Text.Json;
using FileManagementOS.Shared;

public class JsonParser(string path)
{
    // opens the file, parses the whole JSON array into a list, closes the file.
    // File problems throw IOException / UnauthorizedAccessException; bad JSON throws JsonException.
    public List<Person> Parse()
    {
        // "using" closes the file when the method ends, even if parsing throws
        using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);

        List<Person?>? people = JsonSerializer.Deserialize<List<Person?>>(stream, PersonJson.Options);

        if (people is null)
        {
            throw new JsonException("Expected a JSON array of records, but the file contains null.");
        }

        int nullIndex = people.IndexOf(null);
        if (nullIndex >= 0)
        {
            throw new JsonException($"Record {nullIndex + 1} is null instead of an object.");
        }

        return people!;
    }
}
