# FileAnalyzer

The **FileAnalyzer** project is a C# application that analyzes the words in text files (`.txt`, `.docx`, `.pdf`) selected by the user and ranks them by frequency.
This project excludes conjunctions (such as `ve`, `ama`, `fakat`, `hala`, `çünkü`) and special characters while performing word frequency analysis.
All other words, including non-standard words like `xyzxyzxyz`, are counted. After the analysis, the results are displayed on the screen, and a log file is created for the process.

## Features

### Supported File Types:
- 📄 `.txt` (Text File)
- 📄 `.docx` (Word Document)
- 📄 `.pdf` (PDF Document)

### Word Counting:
✅ All words are counted except numbers and specific Turkish conjunctions.  
✅ Words are sorted in descending order based on their frequency.

### Error Logging (Log):
✅ Errors occurring during the process are recorded in a log file.  
✅ The log file is updated after each analysis.

## Getting Started

### Requirements
- 🛠️ **.NET Framework 4.7.2** or later
- 📚 **iTextSharp** (for reading PDF files)
- 📚 **Open XML SDK** (for reading Word documents)

When the project is run, a file selection window is displayed for the user. After selecting a file, the file content is read, and word frequency analysis is performed.

## Installation

### Installing Required Packages:
To run the project, you need to install the necessary NuGet packages. Use the following commands:
```sh
Install-Package iTextSharp
Install-Package DocumentFormat.OpenXml
```

### Running the Project:
1. Open the **FileAnalyzer** project in **Visual Studio**.
2. Click the **Run** button to start the project.
3. A file selection window will open, allowing you to choose the file to be analyzed.
4. Once the file is selected, word frequencies will be analyzed, and frequently used words will be printed to the console.

### Checking the Log File:
- 📜 All errors and logs recorded during the process will be saved in the `log.txt` file.
- 📁 This file is located in the same directory as the project and is updated with each new operation.

## Usage

### File Selection:
🔹 When the project runs, a file selection window appears.  
🔹 `.txt`, `.docx`, and `.pdf` file formats are supported.  
🔹 If an invalid file extension is selected, the user is shown an error message.

### Word Frequency Analysis:
📌 The selected file's content is read, and words are counted, excluding conjunctions and numbers.  
📌 Words are displayed in descending order of frequency.

### Error Logging:
⚠️ Errors that occur during the process are recorded in the `log.txt` file.  
⚠️ The log file is updated with every new operation.

---
🚀 **Happy Coding!** 🚀
