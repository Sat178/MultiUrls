using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiUrls
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            MultiUrls.Utils.Startup.Routine("1.0.0");
            Utils.TikTok.ttUpdate();
            MultiUrls.Utils.Startup.ConvertMode();
            
            Console.ReadKey();
        }
    }
}
