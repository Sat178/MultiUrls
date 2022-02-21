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
            Console.Write("Manually [M] or Automatically [A]");
            //while (!(Console.KeyAvailable && (Console.ReadKey(true).Key == ConsoleKey.M || Console.ReadKey(true).Key == ConsoleKey.A)));

            if (Console.KeyAvailable && (Console.ReadKey(true).Key == ConsoleKey.M)) { Manual(); }
            else if (Console.KeyAvailable && (Console.ReadKey(true).Key == ConsoleKey.A)) { Auto(); }
            else { Console.Clear(); ConvertMode(); }
                


            // Manually

            
        }
        public static void Manual()
        {

        }

        public static void Auto()
        {

        }


    }
}
