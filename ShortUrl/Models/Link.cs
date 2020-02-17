using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortUrl.Models
{
    public class Link
    {
        public static string MainUrl;
        public int Id { get; set; }
        public string ShortUrl { get; set; }
        public string FullUrl { get; set; }
    }
}