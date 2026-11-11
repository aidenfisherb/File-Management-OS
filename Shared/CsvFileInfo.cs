namespace FileManagementOS.Shared;

public static class CsvFileInfo
{
    // count non-empty lines
    public static int CountRecords(string path)
    {
        return File.ReadLines(path).Count(line => line.Length > 0);
    }

    // true if the file is empty or its last byte is '\n'
    public static bool EndsWithNewline(string path)
    {
        using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        if (stream.Length == 0)
        {
            return true;
        }

        stream.Seek(-1, SeekOrigin.End); // jump straight to the last byte; no need to read the whole file
        return stream.ReadByte() == '\n';
    }
}
