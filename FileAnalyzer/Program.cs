using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileAnalyzer
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // Kullanıcıya dosya seçtirilir.
            OpenFileDialog file = new OpenFileDialog();

            while (true)
            {
                // Kullanıcı dosya seçerse işlemler başlar.
                if (file.ShowDialog() == DialogResult.OK)
                {
                    // Seçilen dosyanın adı, uzantısı ve içeriği alınır
                    string fileName = file.FileName;
                    string fileExtension = Path.GetExtension(fileName);
                    string fileContent;

                    // Loglar yapıldı
                    try
                    {
                        fileContent = File.ReadAllText(fileName).ToLower();
                        Log($"Content named \'{fileName}\' is ready");
                    }
                    catch(FileNotFoundException ex)
                    {
                        Console.WriteLine("File connot found");
                        Log($"File cannot be found:{ex.Message}");
                        continue;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"An unknown error has occurred.: {ex.Message}");
                        Log($"An unknown error has occurred: {ex.Message}"); 
                        continue;
                    }


                    // Dosya uzantısının yalnızca .txt veya docx. olduğu kontrol edilir
                    if (fileExtension == ".txt" || fileExtension == ".docx")
                    {
                        // Dosya içeriğindeki kriterlere göre kelimelere ayrılır
                        string [] words = fileContent.Split(new char[] {' ', '.', ',', '!', '?', '\''}, StringSplitOptions.RemoveEmptyEntries);

                        // Her bir özel kelimenin sayısını tutcak Dictionary tanımlanır
                        Dictionary<string, int> wordCounts = new Dictionary<string, int>();

                        foreach(string word in words)
                        {
                            // Bağlaçlar kelimeden sayılmaz
                            if(word != "ve" && word != "ama" && word != "fakat" && word != "hala")
                            {
                                
                                // Sayılar kelimeden sayılmaz
                                if(!int.TryParse(word, out int isNumber))
                                {
                                    // Eğer kelime bulunuyorsa sayısı arttırılır
                                    if (wordCounts.ContainsKey(word))
                                    {
                                        wordCounts[word]++;
                                    }
                                    else // Eğer kelime bulunmuyorsa kelime eklenir ve sayısı 1 olarak belirlenir
                                    {
                                        wordCounts[word] = 1;
                                    }
                                }

                                
                            }

                            
                        }

                        // Kelimeler içerikte bulundukları sayıya göre sıralanır
                        var sortedByCount = wordCounts.OrderByDescending(pair => pair.Value);

                        // Sırası ile kelimeler yazdırılır
                        foreach(var pair in sortedByCount)
                        {
                            Console.WriteLine($"{pair.Key}:{pair.Value}");
                        }

                        break;

                    }
                    else // İstenmeyen uzantıda dosya girilmesi geribildirim verir
                    {
                        Console.WriteLine("Invalid file path. Please select a .txt or .docx file.");
                    }
                }
            }
        }

        static void Log(string message) // Log kaydı yapmak iççin kullanılır
        {
            string logFilePath = "log.txt";
            string logMessage = $"{DateTime.Now}: {message}\n";
            File.AppendAllText(logFilePath, logMessage);
        }
    }


}
