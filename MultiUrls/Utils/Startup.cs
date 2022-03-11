using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace MultiUrls.Utils
{
    internal class Startup
    {
        public static string buildLocation = AppDomain.CurrentDomain.BaseDirectory;
        public static string osTime = DateTime.Now.ToString("dd-MM-yyyy");
        public static bool auto = false;
        public static List<string> rawUrls = new List<string>();

        public static int redditCount = 0;
        public static int twitterCount = 0;
        public static int youtubeCount = 0;
        public static int tiktokCount = 0;

        public static void Routine(string vNumber)
        {
            // Check if program is up to date
            WebClient wc = new WebClient();
                var rawVersion = wc.DownloadString("https://raw.githubusercontent.com/Avoidy/MultiUrls/master/vNumber.txt");
            string upToDate(string isUpdated) => isUpdated == Config.version ? "Newest Version" : "Outdated";
            Console.WriteLine(upToDate(rawVersion));
            wc.Dispose();

            // Automatically create the required directories
            string[] stdDirs = { "Reddit", "Twitter", "TikTok", "Youtube" };
            
            foreach (string smType in stdDirs) // Cycle thru array and create corresponding directory
            {
                string dirLoc = string.Format("{0}\\{1}\\{2}\\{3}", new object[]
                {
                    buildLocation,
                    "MultiUrls",
                    smType,
                    osTime
                });

                if (!Directory.Exists(dirLoc)) { Directory.CreateDirectory(dirLoc); Console.WriteLine($"Creating directory: {dirLoc}"); }
                else { Console.WriteLine($"Directory exists: {dirLoc}"); }

            }
            
            Thread.Sleep(2500); // Gives the user some time to actually read
            Console.Clear();
        }

        public static void ConvertMode()
        {
            Console.Write("Manually [M] or Automatically [A]\n");

            ConsoleKeyInfo cki;
            cki = Console.ReadKey(true); // Read key

            do
            {
                if (cki.Key == ConsoleKey.M)
                    auto = false; Console.Clear(); Worker();
                if (cki.Key == ConsoleKey.A)
                    auto = true; Console.Clear(); Worker();
            }
            while (cki.Key != ConsoleKey.M && cki.Key != ConsoleKey.A); // Read key press
        }
        public static void Worker()
        {
            if (auto == true)
            {
                using (OpenFileDialog ofd = new OpenFileDialog()) // Open file
                {
                    ofd.Title = "Open your url file!";
                    ofd.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    ofd.FilterIndex = 2;
                    ofd.RestoreDirectory = true;

                    bool drOk = ofd.ShowDialog() == DialogResult.OK; // Succeed

                    if (drOk)
                    {
                        string filePath = ofd.FileName;
                        string[] fileUrls = File.ReadAllLines(filePath);
                        foreach (var Url in fileUrls) // Add urls to List<string>
                        {rawUrls.Add(Url); TikTok.totalDL++;}
                        foreach (string Url in rawUrls) // Perform recognision
                            Recognize(Url);
                    }
                }
            }
        }

        public static void Recognize(string Url)
        {
            if (Url.Contains("reddit.com"))
            {
                redditCount++;
                // reddit
            } 
            else if (Url.Contains("tiktok.com"))
            {
                tiktokCount++;
                Utils.TikTok.ttUpdate();
                Utils.TikTok.TikTokAPI(Url,tiktokCount);
                // tiktok
            } 
            else if (Url.Contains("youtube.com"))
            {
                youtubeCount++;
                // youtube
            }
            else if (Url.Contains("twitter.com"))
            {
                twitterCount++;
                // twitter
            }
        }


    }
}
