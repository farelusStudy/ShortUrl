﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShortUrl.Models
{
    /// <summary>
    /// Class give capability to make short unique url
    /// and use it from db
    /// </summary>
    public static class UrlShorter
    {
        static ProjectContext db = new ProjectContext();

        /// <summary>
        /// Service current url before short code
        /// </summary>
        public static string MainUrl { get; set; } = @"https://localhost:44397/S";

        /// <summary>
        /// Find unic short code of full url from db
        /// or make it, if it does not exist
        /// </summary>
        public static Link GetShortedLink(string fullUrl)
        {
            var code = UniqueCode(fullUrl.Length);

            while (IsExists(code)) code = UniqueCode(fullUrl.Length);

            var link = new Link() { FullUrl = fullUrl, ShortUrl = code };

            db.Links.Add(link);
            db.SaveChangesAsync();

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

        private static bool IsExists(string code) => db.Links.Where(l => l.ShortUrl == code).FirstOrDefault() != null;

        private static string UniqueCode(int value)
        {
            value = Math.Sign(value) * value;
            Random random = new Random();
            value = (value == 0) ? random.Next(1, 100) : value;
            string code1 = Base36Extensions.ToBase36((long)Math.Round(value / random.NextDouble()));
            string code2 = Base36Extensions.ToBase36((long)Math.Round(2 * value / random.NextDouble()));
            return code1 + code2;
        }
    }
}