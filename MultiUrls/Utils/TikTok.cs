using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Leaf.xNet;
using Newtonsoft.Json.Linq;

namespace MultiUrls.Utils
{
    internal class TikTok
    {
        public static string postRequest = string.Empty;
        public static List<string> prLoad = new List<string>();
        public static int prAmount = 0;
        public static void TikTokAPI(string req)
        {
            string payload = String.Concat(new string[]
            {
                "url=",
                req
            });

            using (HttpRequest hr = new HttpRequest())
            {
                hr.AllowAutoRedirect = false;
                hr.Proxy = null;
                hr.AddHeader("user-agent", "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/96.0.4664.110 Safari/537.36 RuxitSynthetic/1.0 v8097703692852082201 t7339354177278299067 ath259cea6f altpriv cvcv=2 smf=0This user");

                // Post request

                try
                {
                    postRequest = hr.Post("https://api.tikmate.app/api/lookup", payload, "application/x-www-form-urlencoded; charset=UTF-8").ToString();

                } 
                catch (Exception e)
                {
                    if (postRequest == string.Empty)
                    {
                        Console.WriteLine("Unreachable video!");
                    } 
                    else
                    {
                        Console.WriteLine(e);
                    }
                }

                try
                {
                    JObject jobj = JObject.Parse(postRequest);
                    var tikToken = jobj.SelectToken("token").ToString();
                    var tikID = jobj.SelectToken("id").ToString();
                    var creationDate = jobj.SelectToken("create_time").ToString();


                    string downloadUrl = string.Concat(new string[]
                    {
                    "https://tikmate.app/download/",
                    tikToken,
                    "/",
                    tikID,
                    ".mp4?hd=1"
                    });

                    prAmount++;
                    prLoad.Add(downloadUrl);
                    Task.Run(()=> Downloader(downloadUrl));
                    Console.WriteLine($"Asyncing the download of #{prAmount}: {downloadUrl}");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return;
                }

            }
        }

        public static async void Downloader(string downloadUrl)
        {
            using (WebClient wc = new WebClient())
            {
                Console.WriteLine("Starting Task!");
                wc.DownloadFile(downloadUrl, AppDomain.CurrentDomain.BaseDirectory + $"\\{prAmount}.mp4");
                Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory + $"\\{prAmount}.mp4");
                return;
            }
        }
    }
}
