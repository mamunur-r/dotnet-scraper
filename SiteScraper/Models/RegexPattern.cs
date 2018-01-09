using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public RegexType Type { get; set; }

        public Regex RegularExpression { get; set; }

        public List<KeyValuePair<string, string>> ProcessHtml(string html, List<RegexPattern> regexPatterns)
        {
            var results = new List<KeyValuePair<string, string>>();

            RegexInfo pageTypeMatch = GetPageType(html, regexPatterns, 0);
            // can use reflection 
            if (pageTypeMatch.RegexType.Equals(RegexType.List))
                results = ListPageProcessor(html, regexPatterns, pageTypeMatch);
            return results;
        }

        public List<KeyValuePair<string, string>> ListPageProcessor(string html, List<RegexPattern> regexPatterns, RegexInfo pageTypeMatch)
        {
            var results = new List<KeyValuePair<string, string>>();
            var regexes = regexPatterns.Where(r => r.ParentId.Equals(pageTypeMatch.RegexId) && r.Type.Equals(RegexType.Item)).ToList();
            if (regexes == null) return null;

            var items = GetMatchedResults(html, regexes);

            foreach (var regex in regexes)
            {
                var detailRegexes = regexPatterns.Where(r => r.Type.Equals(RegexType.Detail) && r.ParentId.Equals(regex.Id)).ToList();
                foreach (var item in items)
                {
                    var itemDetail = GetMatchedResults(item.Value, detailRegexes);
                    if (itemDetail != null && itemDetail.Count > 0)
                        results.AddRange(itemDetail);
                }
            }
            return results;
        }

        public List<KeyValuePair<string, string>> GetMatchedResults(string html, List<RegexPattern> regexPatterns)
        {
            List<KeyValuePair<string, string>> results = new List<KeyValuePair<string, string>>();

            foreach (var regex in regexPatterns)
            {
                string groupName = string.Empty;
                try
                {
                    groupName = regex.RegularExpression.GetGroupNames().ToList().Where(gn => Regex.Match(gn, "\\d+").Success != true).FirstOrDefault();

                    foreach (Match m in regex.RegularExpression.Matches(html))
                        results.Add(new KeyValuePair<string, string>(groupName, m.Value));
                }
                catch (Exception)
                {
                }
            }
            return results;
        }



        /// <summary>
        /// returns regex type matched information
        /// </summary>
        /// <param name="html"></param>
        /// <param name="regexPatterns"></param>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public RegexInfo GetPageType(string html, List<RegexPattern> regexPatterns, int parentId)
        {
            var regexes = regexPatterns.Where(r => r.ParentId.Equals(parentId)).ToList();

            foreach (var r in regexes)
                if (r.RegularExpression.Match(html).Success)
                    return new RegexInfo { RegexType = r.Type, ParentId = r.ParentId, RegexId = r.Id };

            return null;
        }

        public Regex GetRegex(string pattern)
        {
            return new Regex(WebUtility.HtmlDecode(pattern), RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }
    }

    public class RegexInfo
    {
        public RegexType RegexType { get; set; }
        public int RegexId { get; set; }
        public int ParentId { get; set; }
    }

    public enum RegexType
    {
        List,
        Selection,
        Item,
        Detail,
        None
    }
}
