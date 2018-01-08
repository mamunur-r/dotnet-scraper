using System;
using System.Collections.Generic;
using System.Text;

namespace WebScraper.Lib
{
    public class ScrapeRequest
    {
        public string Url { get; set; }
        public string RequestType { get; set; }
        public string PostContent { get; set; }
        public WebConfigSetting WebConfig = new WebConfigSetting();
    }
}
