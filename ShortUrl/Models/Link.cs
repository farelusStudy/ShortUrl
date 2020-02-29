using ShortUrl.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ShortUrl.Models
{
    public class Link
    {
        public static string MainUrl = UrlShorter.MainUrl;

        public int Id { get; set; }

        public string ShortUrl { get; set; }

        [Required]
        [DataType(DataType.Url)]
        public string FullUrl { get; set; }

        public string FShortUrl
        {
            get => MainUrl + ShortUrl;
        }
    }
}