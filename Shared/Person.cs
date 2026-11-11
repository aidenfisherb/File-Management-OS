namespace FileManagementOS.Shared;

// person class with necessary fields as strings. 
public sealed record Person(
    string FirstName,
    string LastName,
    string DateOfBirth,
    string Phone,
    string Street,
    string City,
    string State,
    string Zip)
{
    // turns the record into one CSV line: 8 fields joined by commas, no quoting.
    public string ToCsvLine()
    {
        string[] fields = [FirstName, LastName, DateOfBirth, Phone, Street, City, State, Zip];

        foreach (string field in fields)
        {
            if (field.IndexOfAny([',', '"', '\r', '\n']) >= 0)
            {
                throw new InvalidDataException(
                    $"Value \"{field}\" contains a comma, quote, or line break and cannot be written as plain CSV.");
            }
        }

        return string.Join(',', fields);
    }

    // add a toJson method here when needed for demo
}
