﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using SiteScraper.Models;
using WebScraper;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace SiteScraper.Test
{
    [TestClass]
    public class SiteScraperTest
    {
        [TestMethod]
        public void TestGetWebRequestResponseGet()
        {
            WebSession ws = new WebSession();
            WorkItem wi = new WorkItem();
            wi.SiteName = "Stackoverflow";            
            wi.Request.Url = "https://stackoverflow.com/questions/27108264/c-sharp-how-to-properly-make-a-http-web-get-request";
            wi.Request.RequestType = "GET";

            var response = ws.GetWebRequestResponse(wi.Request);
            Assert.IsTrue(response != null);
        }

        [TestMethod]
        public void TestGetWebRequestResponse()
        {
            WebSession ws = new WebSession();
            WorkItem wi = new WorkItem();
            
            wi.Request.Url = "https://www.chamaeleon-reisen.de/start/jx_ajax.php?cube=jx_preistabelle";
            wi.Request.PostContent = "REICODE=ETLAL&showAll=true&showZeitraum=true&dateSpaetester=20.11.2018&dateFruehster=7.01.2018";
            wi.Request.RequestType = "POST";

            var response = ws.GetWebRequestResponse(wi.Request);

            Assert.IsTrue(response != null);
        }

        [TestMethod]
        public void TestGetWebRequestResponseTwo()
        {
            WebSession ws = new WebSession();
            WorkItem wi = new WorkItem();

            wi.Request.Url = "http://www.bambaexperience.com/destinosajax.php";
            wi.Request.PostContent = "rel=1&acc=DD&accx=0&accx2=0";
            wi.Request.RequestType = "POST";

            var response = ws.GetWebRequestResponse(wi.Request);

            Assert.IsTrue(response != null);
        }

        [TestMethod]
        public void TestGetWebClientResponse()
        {
            WebSession ws = new WebSession();
            WorkItem wi = new WorkItem();
            Dictionary<string, string> postContent = new Dictionary<string, string>();

            wi.Request.Url = "https://www.chamaeleon-reisen.de/start/jx_ajax.php?cube=jx_preistabelle";
            wi.Request.RequestType = "POST";


            postContent.Add("REICODE", "ETLAL");
            postContent.Add("showAll", "true");
            postContent.Add("showZeitraum", "true");
            postContent.Add("dateSpaetester", "20.11.2018");
            postContent.Add("dateFruehster", "21.06.2017");

            var response = ws.GetWebClientResponse(wi.Request);

            Assert.IsTrue(response != null);

        }

        [TestMethod]
        public void TestGetWebClientResponseCookie()
        {
            WebSession ws = new WebSession();
            WorkItem wi = new WorkItem();
           
            wi.Request.Url = "https://www.gadventures.co.uk/search/?order_by=-min_price&_pjax=%23trip-results";
            wi.Request.RequestType = "GET";
            //wi.Request.WebConfig.Headers.Add("", "");
            wi.Request.WebConfig.CustomCookies = "cnt=CA;Max-Age=314496000;csrftoken=6276b29e4004240d1b1dd258f4caca11";

            var response = ws.GetWebClientResponse(wi.Request);

            Assert.IsTrue(response != null);
        }

        [TestMethod]
        public void TestGetWebClientResponseOcean()
        {
            WebSession ws = new WebSession();
            WorkItem wi = new WorkItem();

            wi.Request.Url = "https://oceanwide-expeditions.com/antarctica/cruises/pla26-18-antarctic-peninsula";
            wi.Request.RequestType = "GET";
                    
            var response = ws.GetWebClientResponse(wi.Request);

            Assert.IsTrue(response != null);
        }


        [TestMethod]
        public void RegexMatchingTest()
        {
            RegexPattern rp = new RegexPattern();
            string html = File.ReadAllText("Resources\\SampleHtml.txt");
            WorkItem wi = new WorkItem();
            wi.Regexes = new List<RegexPattern>()
            {
                new RegexPattern{
                    Name = "PageSelection",
                    Id = 1,
                    ParentId = 0,
                    Type = RegexType.List,
                    RegularExpression =rp.GetRegex(@"class=""(?<ListPage>cabins-prices\s*custom-width-center)""")
                },
                new RegexPattern{
                    Name = "HtmlSelection",
                    Id = 2,
                    ParentId = 1,
                    Type = RegexType.Selection,
                    RegularExpression =rp.GetRegex(@"(?<Selections><section\s*class=""cabins-prices\s*custom-width-center"".*?</section>)")
                },
                new RegexPattern{
                    Name = "Title",
                    Id = 13,
                    ParentId = 1,
                    Type = RegexType.Global,
                    RegularExpression =rp.GetRegex(@"<h3\s*class=""title""[^>]*>(?<Title>[^<]*)")
                },
                new RegexPattern{
                    Name = "Item",
                    Id = 3,
                    ParentId = 1,
                    Type =RegexType.Item,
                    RegularExpression =rp.GetRegex(@"(?<Item>class=""price-item"".*?<\!--\s*Price\s*item\s*start\s*-->)")
                },
                new RegexPattern{
                    Name = "Name",
                    Id = 4,
                    ParentId = 3,
                    Type =RegexType.Detail,
                    RegularExpression =rp.GetRegex(@"class=""title""[^>]*>(?<Cabin>[^<]*)")
                },
                new RegexPattern{
                    Name = "Date",
                    Id = 5,
                    ParentId = 3,
                    Type =RegexType.Detail,
                    RegularExpression =rp.GetRegex(@"class=""price\s*havent-old-price\s*""[^>]*>(?<Currency>[^\d]*)")
                },
                new RegexPattern{
                    Name = "Price",
                    Id = 6,
                    ParentId = 3,
                    Type =RegexType.Detail,
                    RegularExpression =rp.GetRegex(@"class=""price\s*havent-old-price\s*""[^>]*>\€(?<Price>[^<]*)")
                }
            };

            var response = rp.ProcessHtml(html, wi.Regexes);
            Assert.IsTrue(response != null);
        }


    }
}
