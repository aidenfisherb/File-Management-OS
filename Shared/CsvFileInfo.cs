namespace FileManagementOS.Shared;

//methods for exception handling and file info for CSV files
public static class CsvFileInfo
{
    public static int CountRecords(string path)
    {
        return File.ReadLines(path).Count(line => line.Length > 0);
    }

    public static bool EndsWithNewline(string path)
    {
        using var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        if (stream.Length == 0)
        {
            return true;
        }

        stream.Seek(-1, SeekOrigin.End);
        return stream.ReadByte() == '\n';
    }
}
