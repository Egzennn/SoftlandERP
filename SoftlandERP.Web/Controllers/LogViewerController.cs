using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace SoftlandERP.Web.Controllers
{
    public class LogViewerController : Controller
    {
        public static readonly string? Module = null;
        public static readonly string Name = "Logi";
        public static readonly string ModuleName = Module + Name;
        private readonly List<string> logList;

        public LogViewerController()
        {
            this.logList = new List<string>();
        }

        public List<string> GetLogs()
        {
            return this.logList;
        }

        public void LoadLogsFromFolder(string folderPath)
        {
            try
            {
                // Wyszukujemy pliki logów w folderze
                string[] logFiles = Directory.GetFiles(folderPath, "log-*.txt");

                foreach (string logFile in logFiles)
                {
                    // Odczytujemy zawartość każdego pliku logów i dzielimy go na pola
                    using (StreamReader reader = new StreamReader(logFile))
                    {
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] fields = line.Split(new string[] { " | " }, StringSplitOptions.None);

                            // Dodajemy logi jako pojedynczy wiersz HTML do listy
                            if (fields.Length == 5)
                            {
                                string logEntry = $"<tr><td>{fields[0]}</td><td>{fields[1]}</td><td>{fields[2]}</td><td>{fields[3]}</td><td>{fields[4]}</td></tr>";
                                this.logList.Add(logEntry);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Wystąpił błąd podczas odczytu plików logów: {ex.Message}");
            }
        }

        public IActionResult Index()
        {
            // Przykład wczytania logów z folderu
            this.LoadLogsFromFolder("Logs");

            // Przykład przekazania danych do widoku
            this.ViewBag.Logs = this.GetLogs();

            return this.View(this.GetLogs());
        }
    }
}
