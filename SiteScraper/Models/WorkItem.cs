using System.Collections.Generic;
using WebScraper;

namespace SiteScraper.Models
{
    public class WorkItem
    {
        public string SiteName { get; set; }
        public List<RegexPattern> Regexes = new List<RegexPattern>();
        public ScrapeRequest Request = new ScrapeRequest();           
    }  
}
