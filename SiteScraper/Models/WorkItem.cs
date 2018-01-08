using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebScraper.Lib;

namespace SiteScraper.Models
{
    public class WorkItem
    {
        public string SiteName { get; set; }
        public List<RegexPattern> Regexes = new List<RegexPattern>();
        public ScrapeRequest Request = new ScrapeRequest();           
    }  
}
