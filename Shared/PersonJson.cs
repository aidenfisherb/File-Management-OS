using System.Text.Json;

namespace FileManagementOS.Shared;

// shared JSON settings and the JSON parser, so all three JSON programs agree on keys and format
public static class PersonJson
{
    public static JsonSerializerOptions Options { get; } = new()
    {
        WriteIndented = true,                        // one field per line, easy to read in an editor
        PropertyNameCaseInsensitive = true,          // "firstName" and "FirstName" both parse
        RespectNullableAnnotations = true,           // a field set to null is a parse error
        RespectRequiredConstructorParameters = true, // a missing field is a parse error
    };

    // opens the file, parses the whole JSON array into a list, closes the file.
    // File problems throw IOException / UnauthorizedAccessException; bad JSON throws JsonException.
    public static List<Person> ReadFile(string path)
    {
        // "using" closes the file when the method ends, even if parsing throws
        using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);

        List<Person?>? people = JsonSerializer.Deserialize<List<Person?>>(stream, Options);

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
