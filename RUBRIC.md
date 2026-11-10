Purpose/Overview
In this assignment, your team will write six small programs that create, extend, and read files. You will perform the same three operations in two formats: comma-delimited text (CSV) and JSON. The project shows how applications use operating-system file services and how a file's format changes the way a program reads or adds records.

By the end of this assignment, you should be able to:

Create and write a file in a specified directory, add records without losing existing data, and read the result into a collection.
Explain how your language's file API interacts with the operating system and file system.
Use an appropriate JSON API to serialize and parse structured data.
Verify that records survive a write-and-read cycle and explain how your programs handle common file errors.
Estimated Time
Plan to spend approximately 6 hours researching file APIs, writing and testing the programs, preparing your submission, and rehearsing your demonstration.

Team Requirements
Every team member must participate in research, implementation, and testing. Divide the work as needed, then review both formats together. Each member should be able to run and explain all six programs, including code they did not write.

Assigned Operating System and Programming Language
Use the same assigned operating system and programming language as in the earlier projects unless the instructor gives permission to change. Identify your OS, language or runtime version, file APIs, JSON API, and any dependencies. Document where the programs write their files and how the instructor can reproduce your results.

Instructions
Write six separately runnable programs: a create program, an add program, and a read program for each format. You may share code for record generation, validation, or table display. Use separate output files for CSV and JSON and keep each format's three programs pointed at the same file in a designated project directory.

Record Format
Use fictional or generated data. Each record must contain the following eight fields. Use this order for the CSV columns and for both printed tables:

First name
Last name
Date of birth
Phone number
Street address
City
State
ZIP code
Choose and document consistent field formats. For example, use YYYY-MM-DD for dates. Treat phone numbers and ZIP codes as text so leading zeros and formatting survive when you read the data back. Make the initial and added records distinguishable so you can verify that both batches are present.

Programs 1–3: Comma-Delimited Text (CSV)
Create and write. Create the CSV file in the designated directory and write 100 records. Store one record per line and separate fields with commas. Use no header row in the file so its 100 lines correspond to 100 records. Either generate values without commas, quotation marks, or embedded line breaks, or use a CSV API to handle quoting while keeping each record on one line. Close the file when finished.
Append. Reopen the existing CSV file in append mode and write 50 new records in the same format. Preserve the first 100 records and ensure that the first new record begins on a new line. Close the file. After one create run and one append run, the file must contain 150 records.
Read and display. Open the CSV file for reading, parse all 150 records into a list, array, or equivalent collection, and display the collection as a table with labeled columns. Report the number of records read.
Programs 4–6: JSON
Use an appropriate JSON serialization and parsing API for your language. Store the records as one JSON array of objects. Use consistent keys for the same eight fields in every object.

Create and write. Create a separate JSON file in the designated directory. Serialize 100 records as a JSON array and close the file.
Add records. Read and parse the existing array, add 50 new objects after the original records, and write the updated array back as a valid JSON document. After one create run and one add run, the file must contain 150 records. Appending bytes after the closing bracket of a JSON array would leave invalid JSON; the goal is to add records to the collection while preserving the format.
Read and display. Parse the updated JSON file into a list, array, or equivalent collection and display all 150 records as a table with the same column labels as the CSV table. Report the number of records read.
Verify Your Results
Run each workflow from a fresh project output file. Confirm that it contains 100 records after creation and 150 after one add run.
Verify that the first 100 records retain their original field values and order, and that the 50 new records follow them. Include a sample value with a leading zero to verify that formatting survives.
Try reading a missing file. Report a useful error that identifies the operation and file involved. Document how your code handles malformed input; the JSON add program must report a parse failure before replacing the source file.
Code Requirements
Use the language's file APIs and an appropriate JSON API. Close files when finished, including on error paths where a file was opened. A language construct that closes files automatically is acceptable.
Use clear names for the six programs and label their output. Document the file locations and how to start a fresh run without affecting unrelated files.
Distinguish file access failures from data parsing failures when explaining the code. Identify whether your language reports the errors as exceptions, return values, or another mechanism.
Required Demonstration
Each team will have 30 minutes to demonstrate its programs and explain its design choices.

1. Show Both Workflows
Run the create, add, and read programs for CSV and JSON. Show the 100-to-150 record counts, the labeled tables, and evidence that the original and new records are present in order. You can highlight representative rows during the demonstration; the read programs must display the full tables.

2. Explain the File Operations and Formats
Walk through the code that opens files, writes records, parses them, handles errors, and closes resources. Explain why CSV supports a simple append operation here and why the JSON program reads and updates the array before rewriting it.

Describe the roles of the application, language runtime or library, OS, and file system. Opening a file normally gives the program a handle or stream; it does not by itself load the entire file into application memory. The file system manages file metadata and storage, while the application and its libraries interpret records. Identify the file system used in your demonstration and avoid assuming it uses a File Allocation Table (FAT).

3. Show Error Handling and Explain the Team's Work
Demonstrate a missing-file error and explain what you learned from testing. Describe each member's contribution and one design decision or troubleshooting lesson.

Required Submission
Submit through the Canvas assignment's file upload:

Source code for all six programs and any shared modules or data-generation code.
Setup and run instructions, including the run order, dependencies, output directory, and how to reset the sample files.
The resulting CSV and JSON files with 150 fictional records each.
A brief test record showing both workflows' counts, evidence that the existing records were preserved, and the result of the missing-file test.
A brief team contribution statement and a list of resources used, including any adapted examples or AI assistance.
Instructor Check
Any team member may be asked:

Which open modes does your CSV workflow use, and what would happen if the append program opened the file for replacement?
Why does the JSON add program need to parse the existing data before writing the updated array?
How did you verify that the original records were preserved?
Which parts of the workflow does the OS handle, and which parts does your code or library handle?
How does your program distinguish a missing file from invalid JSON?
Resources
Documentation for your language's file I/O, CSV support if used, and JSON serialization and parsing API.
Documentation for your assigned operating system's file access and the file system used for your output directory.
Course notes and textbook sections on file systems, file access, and storage.
Resources/Tips
Start with a few fictional records to check formatting, then run the required 100-and-50 workflow for your final test.
Check the last original record and the first new record to spot an append boundary error.
Count parsed JSON objects rather than lines in the JSON file.
Academic Integrity
You may consult documentation and learning resources. Identify any code or examples you adapt. If you use AI, describe what it helped with and how you checked the result. Every team member must be able to explain the submitted code and the evidence that it works.

Grading/Rubric
The assignment will be graded on correct file operations, reproducible evidence, and the team's understanding of how the programs work.

Project 4 grading criteria for file operations, CSV and JSON, and team understanding
Strong Submission	Weak Submission
Includes six runnable programs with clear setup instructions.	Omits programs or does not provide a reproducible run order.
Produces 150 records per format while preserving the original 100 and adding 50.	Overwrites, drops, duplicates, or misorders records during the required workflow.
Writes valid CSV and JSON, parses all eight fields, and displays labeled tables.	Produces invalid data, loses field formatting, or cannot read the result back.
Uses a JSON API and explains the difference between appending text and updating a JSON array.	Treats JSON as arbitrary text or leaves extra content outside the array.
Handles file errors and explains the roles of application code, libraries, the OS, and the file system.	Ignores errors or cannot explain how the file operations work.
Provides test evidence and shows that all members contributed and understand both formats.	Gives unsupported claims or relies on one member to explain the work.
Current: Project 4 - File Management (1 of 3), Assignment
Assignment 9A: File System (2 of 3)
Assignment 9B: File Systems (3 of 3)
 
Keep in mind, this submission will count for everyone in your COSC335 OS Teams group.
Choose a submission type