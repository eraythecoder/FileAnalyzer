using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace FileAnalyzer
{
    class Program
    {

        [STAThread]
        static void Main(string[] args)
        {
            // The user is prompted to select a file.
            OpenFileDialog file = new OpenFileDialog();

            while (true)
            {
                // The process begins if the user selects a file.
                if (file.ShowDialog() == DialogResult.OK)
                {
                    // The selected file's name, extension, and content are retrieved.
                    string fileName = file.FileName;
                    string fileExtension = System.IO.Path.GetExtension(fileName);
                    string fileContent = "";

                    // Logs are created
                    try
                    {
                        if (fileExtension == ".txt")
                        {
                            fileContent = File.ReadAllText(fileName).ToLower();
                        }
                        else if (fileExtension == ".docx")
                        {
                            fileContent = ReadDocx(fileName).ToLower();
                        }
                        else if (fileExtension == ".pdf")
                        {
                            fileContent = ReadPdf(fileName).ToLower();
                        }
                        else
                        {
                            Console.WriteLine("Invalid extension");
                            continue;
                        }
                            Log($"Content named '{fileName}' is ready");

                        
                        fileContent = fileContent.Trim();

                        // Split the file content and separate words using necessary special characters and spaces.
                        string[] words = fileContent.Split(new char[] { ' ', '.', ',', '!', '?', '\'', ':', ';', '*', '-', '_', '<', '>', '=', '(', ')', '[', ']', '"', '#', '%', '&', '@', '^', '+', '{', '}', '~', '/', '\\', '|', '`', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);

                        // A Dictionary is defined to keep track of the count of each specific word.
                        Dictionary<string, int> wordCounts = new Dictionary<string, int>();

                        foreach (string word in words)
                        {
                            // Connective words are not counted.
                            if (word != "ve" && word != "ama" && word != "fakat" && word != "hala" && word != "çünkü")
                            {
                                // Numbers are not counted as words.
                                if (!int.TryParse(word, out int isNumber))
                                {
                                    // If the word exists, its count is increased.
                                    if (wordCounts.ContainsKey(word))
                                    {
                                        wordCounts[word]++;
                                    }
                                    else // If the word does not exist, it is added and its count is set to 1.
                                    {
                                        wordCounts[word] = 1;
                                    }
                                }
                            }
                        }

                        // The words are sorted by their frequency in the content.
                        var sortedByCount = wordCounts.OrderByDescending(pair => pair.Value);

                        // The words are printed one by one.
                        foreach (var pair in sortedByCount)
                        {
                            if (pair.Value > 1 && pair.Key.Length > 1)
                            {
                                Console.WriteLine($"{pair.Key}: {pair.Value}");
                            }
                        }

                        break;
                    }
                    catch (FileNotFoundException ex)
                    {
                        Console.WriteLine("File cannot be found");
                        Log($"File cannot be found: {ex.Message}");
                        continue;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An unknown error has occurred: {ex.Message}");
                        Log($"An unknown error has occurred: {ex.Message}");
                        continue;
                    }                    
                }
            }
        }

        static void Log(string message) // Used to create log entries.
        {
            string logFilePath = "log.txt";
            string logMessage = $"{DateTime.Now}: {message}\n";
            File.AppendAllText(logFilePath, logMessage);
        }

        // Method to read .docx files and extract only the text content
        public static string ReadDocx(string filePath)
        {
            StringBuilder text = new StringBuilder();
            using (WordprocessingDocument doc = WordprocessingDocument.Open(filePath, false))
            {
                Body body = doc.MainDocumentPart.Document.Body;
                foreach (var para in body.Elements<Paragraph>())
                {
                    foreach (var run in para.Elements<Run>())
                    {
                        text.Append(run.InnerText);
                        text.Append(" ");
                    }
                }
            }
            return text.ToString();
        }

        public static string ReadPdf(string filePath)
        {
            StringBuilder text = new StringBuilder();
            using (PdfReader reader = new PdfReader(filePath))
            {
                for (int page = 1; page <= reader.NumberOfPages; page++)
                {
                    string pageText = PdfTextExtractor.GetTextFromPage(reader, page);

                    // Cleaning process: removing unnecessary punctuation, numbers, and special characters.
                    pageText = System.Text.RegularExpressions.Regex.Replace(pageText, @"[^\w\s]", ""); // Remove punctuation marks.
                    pageText = System.Text.RegularExpressions.Regex.Replace(pageText, @"\d+", ""); // Remove numbers
                    pageText = System.Text.RegularExpressions.Regex.Replace(pageText, @"\s{2,}", " "); // Replace multiple spaces with a single space.


                    text.Append(pageText);
                }
            }
            return text.ToString().ToLower();
        }

    }
}
