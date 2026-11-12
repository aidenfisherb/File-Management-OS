namespace FileManagementOS.Shared;

// one shared location for the data folder and demos
public static class DataPaths
{
    public static string DataDir { get; } = Path.Combine(Directory.GetCurrentDirectory(), "data");

    public static string CsvFile { get; } = Path.Combine(DataDir, "csv_demo.csv");

    public static string JsonFile { get; } = Path.Combine(DataDir, "json_demo.json");
}
