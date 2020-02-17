using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortUrl.Models
{
    /// <summary>
    /// Class give capability to make short unique url
    /// and use it from db
    /// </summary>
    public static class UrlCutter
    {
        static ProjectContext db = new ProjectContext();

        /// <summary>
        /// Service current url before short code
        /// </summary>
        public static string MainUrl { get; set; } = @"https://localhost:44397/KEK";

        /// <summary>
        /// Find unic short code of full url from db
        /// or make it, if it does not exist
        /// </summary>
        public static Link GetShortedLink(string fullUrl)
        {
            var link = db.Links.Where(l => l.FullUrl == fullUrl).ToList().FirstOrDefault();
            if (link == null) link = MakeAndAddShort(fullUrl);
            return link;
        }

        /// <summary>
        /// Make new short code for new full url and 
        /// add it in db
        /// </summary>
        private static Link MakeAndAddShort(string fullUrl)
        {
            var count = db.Links.ToList().Count;
            count++;
            var shortCode = Base36Extensions.ToBase36(count);
            var link = db.Links.Where(l => l.ShortUrl == shortCode).ToList().FirstOrDefault();
            if (link == null)
            {
                link = new Link() { FullUrl = fullUrl, ShortUrl = shortCode };
                db.Links.Add(link);
                db.SaveChangesAsync();
            }
            return link;
        }

        /// <summary>
        /// Find full link from db by unic short code
        /// </summary>
        /// <param name="shortUrl">unic short code of url</param>
        /// <returns>full url if exsist, else null</returns>
        public static string GetFullUrl(string shortUrl)
        {
            var link = db.Links.Where(l => l.ShortUrl == shortUrl).ToList().FirstOrDefault();
            if (link == null) return null;
            return link.FullUrl;
        }

        private static string UniqueCode(int value)
        {
            value = Math.Sign(value) * value;
            Random random = new Random();
            value = (value == 0) ? random.Next(1, 100) : value;
            string code1 = Base36Extensions.ToBase36(random.Next(100, 10000)*value);
            string code2 = Base36Extensions.ToBase36(random.Next(101, 1000)*value);
            return code1 + code2;
        }
    }
}