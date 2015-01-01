using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            var stringToMatch = @"<a href=""http://builds.kolmafia.us/KoLmafia-";
            using (var wc = new System.Net.WebClient())
            {
                var data = wc.DownloadString("http://builds.kolmafia.us/");
                var matchingLine = data.Split('\n').ToList<String>()
                    .Where(stringToCheck => stringToCheck.Contains(stringToMatch)).FirstOrDefault();
                var indexOfFirstQuote = matchingLine.IndexOf('"') + 1;
                var indexOfSecondQuote = matchingLine.IndexOf('"', indexOfFirstQuote);
                var url = matchingLine.Substring(indexOfFirstQuote, indexOfSecondQuote - indexOfFirstQuote);
                wc.DownloadFile(url, fullPath);
                System.Diagnostics.Process.Start(fullPath);
            }
        }
    }
}
