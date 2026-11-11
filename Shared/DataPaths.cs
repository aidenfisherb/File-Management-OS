namespace FileManagementOS.Shared;

// one shared location for the data folder and demos
public static class DataPaths
{
    public static string DataDir { get; } = Path.Combine(Directory.GetCurrentDirectory(), "data");

    public static string CsvFile { get; } = Path.Combine(DataDir, "csv_demo.csv");

    // add a json file path here when needed for demo
}
