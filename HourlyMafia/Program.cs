using System;
using System.Linq;

namespace HourlyMafia
{
    class Program
    {
        static void Main(string[] args)
        {
            var path = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var directory = System.IO.Path.GetDirectoryName(path);
            var fullPath = directory + "\\KolMafia.jar";
            System.IO.File.Delete(fullPath);
            Console.WriteLine("Downloading latest hourly build.,,");
            var stringToMatch = @"<a href=""http://builds.kolmafia.us/KoLmafia-";
            using (var wc = new System.Net.WebClient())
            {
                var data = wc.DownloadString("http://builds.kolmafia.us/");
                var matchingLine = data.Split('\n')
                    .Where(stringToCheck => stringToCheck.Contains(stringToMatch)).FirstOrDefault();
                var indexOfURL = matchingLine.IndexOf('"') + 1;
                var indexOfSecondQuote = matchingLine.IndexOf('"', indexOfURL);
                var url = matchingLine.Substring(indexOfURL, indexOfSecondQuote - indexOfURL);
                wc.DownloadFile(url, fullPath);
                Console.WriteLine("Download complete.");
            }
            Console.WriteLine("Launching latest build.");
            System.Diagnostics.Process.Start(fullPath);
        }
    }
}
