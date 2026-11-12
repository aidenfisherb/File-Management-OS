# File-Management-OS

Roles:

- Ethan: Handle CSV parsing and shared helper methods, including converting CSV rows into records and validating or formatting data.
- Aiden: Create and produce the CSV file, write the initial records, and append new records to the existing CSV file.
- Jake: Handle JSON file creation and serialization, including generating the initial records and writing them as a JSON array.
- Aidan: Handle JSON parsing and record updates, including reading the existing array, adding 50 records, rewriting the valid JSON file, and reading/displaying the final records.

## Setup and run

- OS: Windows 11, NTFS file system. Language: C# on .NET 10 (SDK 10.0.400, pinned in `global.json`). No extra packages.
- Each program is a standalone .NET 10 file-based app. Shared code (record type, generator, file path) is in `Shared/`. Run from this folder:

| # | Program | Command |
|---|---|---|
| 1 | CSV Create | `dotnet run Csv/CsvCreate.cs` |
| 2 | CSV Append | `dotnet run Csv/CsvAppend.cs` |
| 3 | CSV Read | `dotnet run Csv/CavParserDemo.cs` |
| 4 | JSON Create | `dotnet run Json/JsonCreate.cs` |
| 5 | JSON Add | `dotnet run Json/JsonAdd.cs` |
| 6 | JSON Read | `dotnet run Json/JsonRead.cs` |

- A new program file starts with `#:project ../Shared` and `#:property PublishAot=false` (see `Csv/CsvCreate.cs`).
- Output file: `data/csv_demo.csv` (no header row, one record per line).
- Output file: `data/json_demo.json` (one JSON array of objects; keys `FirstName`, `LastName`, `DateOfBirth`, `Phone`, `Street`, `City`, `State`, `Zip`, all strings). JSON API: `System.Text.Json` (built into .NET). All three JSON programs use the path in `DataPaths.JsonFile` and the settings in `PersonJson.Options` (`Shared/PersonJson.cs`) so the keys match.
- JSON Add parses the whole file before writing anything. If the file is missing or is not valid JSON, it reports the error and leaves the file unchanged. Each run of JSON Add adds 50 more records, so run JSON Create first for a fresh 100 → 150. To reset fully, delete `data/json_demo.json`.
- Field formats: date of birth `YYYY-MM-DD`; phone `205-555-####` (initial batch `1001`–`1100`, added batch `2001`–`2050`); ZIP is 5-digit text (leading zeros kept).
- Fresh run: CSV Create replaces the file, so running Create then Append always gives 100 → 150. To reset fully, delete `data/csv_demo.csv`.
