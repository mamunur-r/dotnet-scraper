﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace WebScraper.Lib
{
    public class WebConfigSetting
    {
        public string Host { get; set; }
        public string Accept { get; set; }
        public string Referer { get; set; }
        public string UserAgent { get; set; }
        /// <summary>
        /// Request timeout period in ms
        /// Default is 90000
        /// </summary>
        public int TimeOut = 90000;
        public Dictionary<string, string> Headers;
        public CookieCollection Cookies;
    }
}
