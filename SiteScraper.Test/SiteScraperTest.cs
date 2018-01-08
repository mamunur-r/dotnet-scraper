using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SiteScraper.Models;
using WebScraper;
using System.Collections.Generic;

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
    }
}
