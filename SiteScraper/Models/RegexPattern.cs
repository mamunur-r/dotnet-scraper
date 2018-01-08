using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SiteScraper.Models
{
    public class RegexPattern
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public int ParentId { get; set; }

        public string Type { get; set; }

        public Regex RegularExpression { get; set; }
    }
}
