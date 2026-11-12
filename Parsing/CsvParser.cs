using FileManagementOS.Shared;
using Microsoft.VisualBasic.FileIO;

public partial class CsvParser(string path)
{
    public List<Person> Parse()
    {
        var list = new List<Person>();
        using var p = new TextFieldParser(path);
        p.SetDelimiters(",");
        while (!p.EndOfData)
        {
            var f = p.ReadFields()!;
            if (f.Length != 8)
                throw new InvalidDataException($"Expected 8 fields, got {f.Length} at line {p.LineNumber}.");
            list.Add(new Person(f[0], f[1], f[2], f[3], f[4], f[5], f[6], f[7]));
        }
        return list;
    }
}
