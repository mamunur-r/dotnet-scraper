using Newtonsoft.Json;
using SiteScraper.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebScraper;

namespace SiteScraper
{
    class Program
    {
        static void Main(string[] args)
        {

            //WorkItem wi = new WorkItem();
            //RegexPattern rp = new RegexPattern();
            //wi.SiteName = "OcenWide";
            //wi.Regexes = new List<RegexPattern>()
            //{
            //    new RegexPattern{
            //        Name = "PageSelection",
            //        Id = 1,
            //        ParentId = 0,
            //        Type = RegexType.List,
            //        RegularExpression =rp.GetRegex(@"class=""(?<ListPage>cabins-prices\s*custom-width-center)""")
            //    },
            //    new RegexPattern{
            //        Name = "HtmlSelection",
            //        Id = 2,
            //        ParentId = 1,
            //        Type = RegexType.Selection,
            //        RegularExpression =rp.GetRegex(@"(?<Selections><section\s*class=""cabins-prices\s*custom-width-center"".*?</section>)")
            //    },
            //    new RegexPattern{
            //        Name = "Title",
            //        Id = 13,
            //        ParentId = 1,
            //        Type = RegexType.Global,
            //        RegularExpression =rp.GetRegex(@"<h3\s*class=""title""[^>]*>(?<Title>[^<]*)")
            //    },
            //    new RegexPattern{
            //        Name = "Item",
            //        Id = 3,
            //        ParentId = 1,
            //        Type =RegexType.Item,
            //        RegularExpression =rp.GetRegex(@"(?<Item>class=""price-item"".*?<\!--\s*Price\s*item\s*start\s*-->)")
            //    },
            //    new RegexPattern{
            //        Name = "Name",
            //        Id = 4,
            //        ParentId = 3,
            //        Type =RegexType.Detail,
            //        RegularExpression =rp.GetRegex(@"class=""title""[^>]*>(?<Cabin>[^<]*)")
            //    },
            //    new RegexPattern{
            //        Name = "Date",
            //        Id = 5,
            //        ParentId = 3,
            //        Type =RegexType.Detail,
            //        RegularExpression =rp.GetRegex(@"class=""price\s*havent-old-price\s*""[^>]*>(?<Currency>[^\d]*)")
            //    },
            //    new RegexPattern{
            //        Name = "Price",
            //        Id = 6,
            //        ParentId = 3,
            //        Type =RegexType.Detail,
            //        RegularExpression =rp.GetRegex(@"class=""price\s*havent-old-price\s*""[^>]*>.(?<Price>[^<]*)")
            //    }
            //};
            //wi.Request.Url = "https://oceanwide-expeditions.com/antarctica/cruises/otl27-18-antarctic-peninsula";
            //wi.Request.RequestType = "GET";

            //string json = JsonConvert.SerializeObject(wi);

            //WebSession ws = new WebSession();
            //List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            //var html = ws.GetWebClientResponse(wi.Request);
            //if (!string.IsNullOrEmpty(html))
            //    results = rp.ProcessHtml(html, wi.Regexes);


            WebSession ws = new WebSession();
            RegexPattern rp = new RegexPattern();
            List<Dictionary<string, string>> results = new List<Dictionary<string, string>>();

            var workItem = JsonConvert.DeserializeObject<WorkItem>(File.ReadAllText("Resources\\Config.json"));

            var html = ws.GetWebClientResponse(workItem.Request);
            if (!string.IsNullOrEmpty(html))
                results = rp.ProcessHtml(html, workItem.Regexes);

            SaveAsCSV(results);
        }

        public static void SaveAsCSV(List<Dictionary<string, string>> results)
        {           
            if (!results.Any()) return;

            StringBuilder sb = new StringBuilder();
            List<string> headers = results[0].Keys.Select(k => k).OrderBy(k => k).ToList();
            sb.AppendLine(string.Join(",", headers.Select(h => h)));

            foreach(var r in results)            
                sb.AppendLine(string.Join(",", headers.Select(h=> r[h].Replace(","," "))));
            File.WriteAllText("C:\\temp\\results.csv", sb.ToString());
            
        }
    }
}
