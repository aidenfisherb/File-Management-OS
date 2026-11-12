using System.Text.Json;

namespace FileManagementOS.Shared;

// shared JSON settings, so all three JSON programs agree on keys and format
public static class PersonJson
{
    public static JsonSerializerOptions Options { get; } = new()
    {
        WriteIndented = true,                        // one field per line, easy to read in an editor
        PropertyNameCaseInsensitive = true,          // "firstName" and "FirstName" both parse
        RespectNullableAnnotations = true,           // a field set to null is a parse error
        RespectRequiredConstructorParameters = true, // a missing field is a parse error
    };
}
